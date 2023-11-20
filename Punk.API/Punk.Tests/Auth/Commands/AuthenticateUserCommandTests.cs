using FluentValidation.TestHelper;
using Moq;
using Punk.Application.Features.Auth.Commands;
using Punk.Domain.Interfaces.Repositories;
using Punk.Application.Services;
using Punk.Domain.Models;
using Punk.Services.Helpers;
using static NUnit.Framework.Assert;

namespace Punk.Tests
{
    [TestFixture]
    public class AuthenticateUserCommandTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IPasswordService> _mockPasswordService;
        private Mock<IJwtService> _mockJwtService;
        private AuthenticateUserCommandHandler _handler;
        private AuthenticateUserCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordService = new Mock<IPasswordService>();
            _mockJwtService = new Mock<IJwtService>();
            _handler = new AuthenticateUserCommandHandler(_mockUserRepository.Object, _mockPasswordService.Object,
                _mockJwtService.Object);
            _validator = new AuthenticateUserCommandValidator();
        }

        #region Handler Tests

        [Test]
        public async Task Handle_ShouldReturnUserDto_WhenCredentialsAreValid()
        {
            var request = new AuthenticateUserCommand { Email = "test@example.com", Password = "ValidPassword123!" };
            var user = CreateTestUser();
            _mockUserRepository.Setup(repo => repo.GetByUsernameAsync(request.Email)).ReturnsAsync(user);
            _mockPasswordService
                .Setup(service => service.VerifyPassword(request.Password, user.PasswordHash, user.Salt, 10000, 20))
                .Returns(true);
            _mockJwtService.Setup(service => service.GenerateTokenForUser(It.IsAny<User>())).Returns("Token");
            _mockJwtService.Setup(service => service.GenerateRefreshToken()).Returns("RefreshToken");

            var result = await _handler.Handle(request, CancellationToken.None);

            NotNull(result);
            Assert.Multiple(() =>
            {
                That(result.Token, Is.EqualTo("Token"));
                That(result.RefreshToken, Is.EqualTo("RefreshToken"));
                That(result.Username, Is.EqualTo("testuser@test.com"));
                That(result.Name, Is.EqualTo("Test User"));
            });
        }

        #endregion

        #region Validator Tests

        [Test]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var command = new AuthenticateUserCommand() { Email = "testuser@test.com@test.com" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }


        [Test]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var command = new AuthenticateUserCommand { Email = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        #endregion

        private User CreateTestUser()
        {
            var testPassword = "TestPassword123!";
            var salt = _mockPasswordService.Object.GenerateSalt();
            var passwordHash = _mockPasswordService.Object.HashPassword(testPassword, salt);

            return new User
            {
                Id = Guid.NewGuid(),
                Username = "testuser@test.com",
                Name = "Test User",
                PasswordHash = passwordHash,
                Salt = salt,
                RefreshToken = "testtoken",
                RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(1),
                FavouriteBeers = new List<FavouriteBeer>()
            };
        }
    }
}