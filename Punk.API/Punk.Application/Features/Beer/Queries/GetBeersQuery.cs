using FluentValidation;
using MediatR;
using Punk.Application.Dtos;
using Punk.Domain.Interfaces.Services;

namespace Punk.Application.Features.Beer.Queries;

public class GetBeersQuery : IRequest<IEnumerable<BeerDto>>
{
    public string Ids { get; set; } 
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 25;
}

public class GetBeersQueryHandler : IRequestHandler<GetBeersQuery, IEnumerable<BeerDto>>
{
    private readonly IBeerApiService _beerApiService;

    public GetBeersQueryHandler(IBeerApiService beerApiService)
    {
        _beerApiService = beerApiService;
    }

    public async Task<IEnumerable<BeerDto>> Handle(GetBeersQuery request, CancellationToken cancellationToken)
    {
        var ids = request.Ids?
            .Split(',')
            .Select(s => int.TryParse(s, out int id) ? (int?)id : null)
            .Where(id => id.HasValue)
            .Select(id => id.Value)
            .ToList();

        var beers = await _beerApiService.GetBeersAsync(request.Page, request.PageSize, ids);
        return beers;
    }
}

public class GetBeersQueryValidator : AbstractValidator<GetBeersQuery>
{
    public GetBeersQueryValidator()
    {
        RuleFor(x => x.PageSize).InclusiveBetween(1, 25).WithMessage("Page size must be between 1 and 25.");
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");
    }
}