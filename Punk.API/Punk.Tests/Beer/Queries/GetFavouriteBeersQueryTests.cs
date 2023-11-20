using FluentValidation.TestHelper;
using Moq;
using Punk.Application.Features.Beer.Queries;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Interfaces.Services;
using Punk.Application.Dtos;
using Punk.Application.Services;
using Punk.Domain.Models;
using Punk.Tests.Beer;
using static NUnit.Framework.Assert;


namespace Punk.Tests
{
    [TestFixture]
    public class GetFavouriteBeersQueryHandlerTests
    {
        private Mock<IBeerRepository> _mockBeerRepository;
        private Mock<IJwtService> _mockJwtService;
        private Mock<IBeerApiService> _mockBeerApiService;
        private GetFavouriteBeersQueryHandler _handler;
        private GetFavouriteBeersQueryValidator _validator;

        [SetUp]
        public void Setup()
        {
            _mockBeerRepository = new Mock<IBeerRepository>();
            _mockJwtService = new Mock<IJwtService>();
            _mockBeerApiService = new Mock<IBeerApiService>();
            _handler = new GetFavouriteBeersQueryHandler(_mockBeerApiService.Object, _mockBeerRepository.Object,
                _mockJwtService.Object);
            _validator = new GetFavouriteBeersQueryValidator();
        }


        #region Handler Tests

        [Test]
        public async Task Handle_ShouldReturnBeers_WhenBeersAreFound()
        {
            var query = new GetFavouriteBeersQuery("validToken", 1, 10);
            var userId = Guid.NewGuid();
            _mockJwtService.Setup(s => s.GetUserIdFromToken(It.IsAny<string>())).Returns(userId);
            var favoriteBeers = new List<FavouriteBeer>
            {
                new() { BeerId = 1, UserId = userId },
                new() { BeerId = 2, UserId = userId }
            };
            _mockBeerRepository.Setup(repo => repo.GetUserFavoriteBeers(userId, query.Page, query.PageSize))
                .ReturnsAsync(favoriteBeers);
            var beerDtos = new List<BeerDto> { Helpers.CreateBeer(1), Helpers.CreateBeer(2) };
            _mockBeerApiService
                .Setup(service => service.GetBeersAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<List<int>>()))
                .ReturnsAsync(beerDtos);

            var result = await _handler.Handle(query, CancellationToken.None);

            var firstResult = result.FirstOrDefault();

            Multiple(() =>
            {
                That(result, Is.Not.Null);
                That(result, Is.Not.Empty);
                That(firstResult?.Id, Is.EqualTo(1));
                That(firstResult?.Name, Is.EqualTo("Test Beer"));
                That(firstResult?.Tagline, Is.EqualTo("A Test Beer"));
                That(firstResult?.Description, Is.EqualTo("This is a test description for a beer."));
                That(firstResult?.ImageUrl, Is.EqualTo("https://example.com/test_beer.jpg"));
                That(firstResult?.Abv, Is.EqualTo(5.0));
                That(firstResult?.Ibu, Is.EqualTo(15.0));
                That(firstResult?.Ingredients.Malt.Any(), Is.True);
                That(firstResult?.Ingredients.Hops.Any(), Is.True);
                That(firstResult?.Ingredients.Yeast, Is.Not.Empty);
            });
        }

        #endregion

        #region Validator Tests

        [Test]
        public void Should_Have_Error_When_Token_Is_Empty()
        {
            var query = new GetFavouriteBeersQuery(string.Empty);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Token);
        }

        [Test]
        public void Should_Have_Error_When_PageSize_Is_OutOfRange()
        {
            var query = new GetFavouriteBeersQuery("validToken", 1, 30);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Test]
        public void Should_Have_Error_When_Page_Is_LessThanOne()
        {
            var query = new GetFavouriteBeersQuery("validToken", 0, 10);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Page);
        }

        [Test]
        public void Should_Not_Have_Error_When_Query_Is_Valid()
        {
            var query = new GetFavouriteBeersQuery("validToken", 1, 10);
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}