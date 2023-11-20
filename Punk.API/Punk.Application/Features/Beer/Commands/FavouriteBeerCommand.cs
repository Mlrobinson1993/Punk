using MediatR;
using FluentValidation;
using Punk.Domain.Interfaces.Repositories;

namespace Punk.Application.Features.Beer.Commands;

public class FavouriteBeerCommand : IRequest
{
    public Guid UserId { get; set; }
    public int BeerId { get; set; }
    public bool IsFavourite { get; set; }
}

public class FavouriteBeerCommandHandler : IRequestHandler<FavouriteBeerCommand>
{
    private readonly IBeerRepository _beerRepository;

    public FavouriteBeerCommandHandler(IBeerRepository beerRepository)
    {
        _beerRepository = beerRepository;
    }

    public async Task Handle(FavouriteBeerCommand request, CancellationToken cancellationToken)
    {
        if (request.IsFavourite)
        {
            await _beerRepository.FavouriteBeer(request.UserId, request.BeerId);
            return;
        }

        await _beerRepository.UnfavouriteBeer(request.UserId, request.BeerId);
    }
}

public class FavouriteBeerCommandValidator : AbstractValidator<FavouriteBeerCommand>
{
    public FavouriteBeerCommandValidator()
    {
        RuleFor(command => command.UserId).NotEmpty().WithMessage("Validation error occured.");
        RuleFor(command => command.BeerId).NotEmpty().WithMessage("Validation error occured.");
        RuleFor(command => command.IsFavourite).NotEmpty().WithMessage("Validation error occured.");
    }
}