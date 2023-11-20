using FluentValidation;
using MediatR;
using Punk.Application.Dtos;
using Punk.Common.Exceptions;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Interfaces.Services;

namespace Punk.Application.Features.Beer.Queries;

public class GetBeerByNameQuery : IRequest<List<BeerDto>>
{
    public GetBeerByNameQuery(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}

public class GetBeerByNameQueryHandler : IRequestHandler<GetBeerByNameQuery, List<BeerDto>>
{
    private readonly IBeerApiService _beerApiService;

    public GetBeerByNameQueryHandler(IBeerApiService beerApiService)
    {
        _beerApiService = beerApiService;
    }


    public async Task<List<BeerDto>> Handle(GetBeerByNameQuery request, CancellationToken cancellationToken)
    {
        var name = request.Name.Replace(" ", "_");

        var beers = await _beerApiService.GetBeerAsync(name);

        if (!beers.Any())
            throw new NotFoundException("No beers found with name " + request.Name);

        return beers;
    }
}

public class GetBeerByNameQueryValidator : AbstractValidator<GetBeerByNameQuery>
{
    public GetBeerByNameQueryValidator()
    {
        RuleFor(query => query.Name).NotEmpty().WithMessage("Beer name is required.");
    }
}