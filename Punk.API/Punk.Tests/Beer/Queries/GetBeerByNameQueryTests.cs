using FluentValidation.TestHelper;
using Moq;
using Punk.Application.Features.Beer.Queries;
using Punk.Domain.Interfaces.Services;
using Punk.Application.Dtos;
using Punk.Common.Exceptions;
using Punk.Tests.Beer;
using static NUnit.Framework.Assert;

namespace Punk.Tests
{
    [TestFixture]
    public class GetBeerByNameQueryHandlerTests
    {
        private Mock<IBeerApiService> _mockBeerApiService;
        private GetBeerByNameQueryHandler _handler;
        private GetBeerByNameQueryValidator _validator;


        [SetUp]
        public void Setup()
        {
            _mockBeerApiService = new Mock<IBeerApiService>();
            _handler = new GetBeerByNameQueryHandler(_mockBeerApiService.Object);
            _validator = new GetBeerByNameQueryValidator();
        }


        #region Handler Tests

        [Test]
        public async Task Handle_ShouldReturnBeers_WhenBeersAreFound()
        {
            var query = new GetBeerByNameQuery("TestBeer");
            var id = 1;
            var beers = new List<BeerDto> { Helpers.CreateBeer(id) };
            _mockBeerApiService.Setup(s => s.GetBeerAsync(It.IsAny<string>())).ReturnsAsync(beers);


            var result = await _handler.Handle(query, CancellationToken.None);

            var firstResult = result.FirstOrDefault();
            Assert.Multiple(() =>
            {
                That(result, Is.Not.Null);
                That(result, Is.Not.Empty);
                That(firstResult?.Id, Is.EqualTo(id));
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

        [Test]
        public void Handle_ShouldThrowNotFoundException_WhenNoBeersAreFound()
        {
            var query = new GetBeerByNameQuery("UnknownBeer");
            _mockBeerApiService.Setup(s => s.GetBeerAsync(It.IsAny<string>())).ReturnsAsync(new List<BeerDto>());

            ThrowsAsync<NotFoundException>(async () => await _handler.Handle(query, CancellationToken.None));
        }

        #endregion

        #region Validator Tests

        [Test]
        public void Should_Have_Error_When_Name_Is_Empty()
        {
            var query = new GetBeerByNameQuery(string.Empty);
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Test]
        public void Should_Not_Have_Error_When_Name_Is_Provided()
        {
            var query = new GetBeerByNameQuery("TestBeer");
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
        }

        #endregion
    }
}