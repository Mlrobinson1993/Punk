using FluentValidation.TestHelper;
using Moq;
using Punk.Application.Features.Beer.Queries;
using Punk.Domain.Interfaces.Services;
using Punk.Application.Dtos;
using static NUnit.Framework.Assert;


namespace Punk.Tests
{
    [TestFixture]
    public class GetBeersQueryHandlerTests
    {
        private Mock<IBeerApiService> _mockBeerApiService;
        private GetBeersQueryHandler _handler;
        private GetBeersQueryValidator _validator;

        [SetUp]
        public void Setup()
        {
            _mockBeerApiService = new Mock<IBeerApiService>();
            _handler = new GetBeersQueryHandler(_mockBeerApiService.Object);
            _validator = new GetBeersQueryValidator();
        }

        #region Handler Tests

        [Test]
        public async Task Handle_ShouldReturnBeers_WhenCalledWithValidQuery()
        {
            var query = new GetBeersQuery { Page = 1, PageSize = 10 };
            var beers = new List<BeerDto> { new BeerDto() /* ... initialize properties ... */ };
            _mockBeerApiService.Setup(s => s.GetBeersAsync(query.Page, query.PageSize, It.IsAny<List<int>>()))
                .ReturnsAsync(beers);
            
            var result = await _handler.Handle(query, CancellationToken.None);

            IsNotNull(result);
            IsNotEmpty(result);
        }

        #endregion

        #region Validator Tests

        [Test]
        public void Should_Have_Error_When_PageSize_Is_OutOfRange()
        {
            var query = new GetBeersQuery { Page = 1, PageSize = 30 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.PageSize);
        }

        [Test]
        public void Should_Have_Error_When_Page_Is_LessThanOne()
        {
            var query = new GetBeersQuery { Page = 0, PageSize = 10 };
            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Page);
        }

        [Test]
        public void Should_Not_Have_Error_When_Query_Is_Valid()
        {
            var query = new GetBeersQuery { Page = 1, PageSize = 10 };
            var result = _validator.TestValidate(query);
            result.ShouldNotHaveAnyValidationErrors();
        }

        #endregion
    }
}