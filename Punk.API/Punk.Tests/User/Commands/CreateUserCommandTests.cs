using FluentValidation.TestHelper;
using Moq;
using Punk.Application.Features.Users.Commands;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Models;
using Punk.Services.Helpers;
using Punk.Common.Exceptions;

namespace Punk.Tests
{
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IPasswordService> _mockPasswordService;
        private CreateUserCommandHandler _handler;
        private CreateUserCommandValidator _validator;

        [SetUp]
        public void Setup()
        {
            _mockUserRepository = new Mock<IUserRepository>();
            _mockPasswordService = new Mock<IPasswordService>();
            _handler = new CreateUserCommandHandler(_mockUserRepository.Object, _mockPasswordService.Object);
            _validator = new CreateUserCommandValidator();
        }

        #region Handler Tests

        [Test]
        public async Task Handle_ShouldReturnTrue_WhenUserIsCreatedSuccessfully()
        {
            var command = new CreateUserCommand
                { Name = "John Doe", Email = "john@example.com", Password = "password123" };
            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(true);
            
            var result = await _handler.Handle(command, CancellationToken.None);
            
            Assert.IsTrue(result);
            _mockUserRepository.Verify(repo => repo.CreateAsync(It.IsAny<User>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowBadRequestException_WhenUserCreationFails()
        {

            var command = new CreateUserCommand
                { Name = "Jane Doe", Email = "jane@example.com", Password = "password123" };
            _mockUserRepository.Setup(repo => repo.CreateAsync(It.IsAny<User>())).ReturnsAsync(false);
            
            Assert.ThrowsAsync<BadRequestException>(async () => await _handler.Handle(command, CancellationToken.None));
        }

        #endregion

        #region Validator Tests

        [Test]
        public void Should_Have_Error_When_Email_Is_Empty()
        {
            var command = new CreateUserCommand { Email = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Should_Have_Error_When_Email_Is_Invalid()
        {
            var command = new CreateUserCommand { Email = "invalidemail" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Test]
        public void Should_Have_Error_When_Password_Is_Empty()
        {
            var command = new CreateUserCommand { Password = string.Empty };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Have_Error_When_Password_Is_Too_Short()
        {
            var command = new CreateUserCommand { Password = "Short1!" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Have_Error_When_Password_Is_Too_Long()
        {
            var command = new CreateUserCommand { Password = "VeryLongPassword1!" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Have_Error_When_Password_Lacks_Uppercase()
        {
            var command = new CreateUserCommand { Password = "lowercase1!" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Have_Error_When_Password_Lacks_Lowercase()
        {
            var command = new CreateUserCommand { Password = "UPPERCASE1!" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Have_Error_When_Password_Lacks_Number()
        {
            var command = new CreateUserCommand { Password = "NoNumber!" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Have_Error_When_Password_Lacks_Special_Character()
        {
            var command = new CreateUserCommand { Password = "NoSpecial1" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Password);
        }

        [Test]
        public void Should_Not_Have_Error_When_Command_Is_Valid()
        {
            var command = new CreateUserCommand
            {
                Name = "test user",
                Email = "test@test.com",
                Password = "ValidPass1!"
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}