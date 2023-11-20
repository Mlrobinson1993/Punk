using FluentValidation.TestHelper;
using Moq;
using Punk.Application.Features.Beer.Commands;
using Punk.Domain.Interfaces.Repositories;

namespace Punk.Tests.Beer.Commands;

[TestFixture]
public class FavouriteBeerCommandTests
{
    private Mock<IBeerRepository> _mockBeerRepository;
    private FavouriteBeerCommandHandler _handler;
    private FavouriteBeerCommandValidator _validator;

    [SetUp]
    public void Setup()
    {
        _mockBeerRepository = new Mock<IBeerRepository>();
        _handler = new FavouriteBeerCommandHandler(_mockBeerRepository.Object);
        _validator = new FavouriteBeerCommandValidator();
    }

    #region Handler Tests

    [Test]
    public async Task Handle_ShouldCallFavouriteBeer_WhenIsFavouriteIsTrue()
    {
        var command = new FavouriteBeerCommand { UserId = Guid.NewGuid(), BeerId = 1, IsFavourite = true };


        await _handler.Handle(command, CancellationToken.None);


        _mockBeerRepository.Verify(x => x.FavouriteBeer(command.UserId, command.BeerId), Times.Once());
    }

    [Test]
    public async Task Handle_ShouldCallUnfavouriteBeer_WhenIsFavouriteIsFalse()
    {
        var command = new FavouriteBeerCommand { UserId = Guid.NewGuid(), BeerId = 1, IsFavourite = false };

        await _handler.Handle(command, CancellationToken.None);

        _mockBeerRepository.Verify(x => x.UnfavouriteBeer(command.UserId, command.BeerId), Times.Once());
    }

    #endregion

    #region Validator Tests

    [Test]
    public void Should_Have_Error_When_UserId_Is_Empty()
    {
        var command = new FavouriteBeerCommand { UserId = Guid.Empty, BeerId = 1, IsFavourite = true };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.UserId);
    }

    [Test]
    public void Should_Have_Error_When_BeerId_Is_Zero()
    {
        var command = new FavouriteBeerCommand { UserId = Guid.NewGuid(), BeerId = 0, IsFavourite = true };
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(c => c.BeerId);
    }

    [Test]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        var command = new FavouriteBeerCommand { UserId = Guid.NewGuid(), BeerId = 1, IsFavourite = true };
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }

    #endregion
}