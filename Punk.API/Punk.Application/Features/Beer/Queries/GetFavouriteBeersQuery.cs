using FluentValidation;
using MediatR;
using Punk.Application.Dtos;
using Punk.Application.Services;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Interfaces.Services;

namespace Punk.Application.Features.Beer.Queries;

public class GetFavouriteBeersQuery : IRequest<IEnumerable<BeerDto>>
{
    public GetFavouriteBeersQuery(string token, int page = 1, int pageSize = 25)
    {
        Token = token;
        Page = page;
        PageSize = pageSize;
    }


    public string Token { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}

public class GetFavouriteBeersQueryHandler : IRequestHandler<GetFavouriteBeersQuery, IEnumerable<BeerDto>>
{
    private readonly IBeerRepository _beerRepository;
    private readonly IJwtService _jwtService;
    private readonly IBeerApiService _beerApiService;

    public GetFavouriteBeersQueryHandler(
        IBeerApiService beerApiService,
        IBeerRepository beerRepository,
        IJwtService jwtService)
    {
        _jwtService = jwtService;
        _beerRepository = beerRepository;
        _beerApiService = beerApiService;
    }

    public async Task<IEnumerable<BeerDto>> Handle(GetFavouriteBeersQuery request, CancellationToken cancellationToken)
    {
        var userId = _jwtService.GetUserIdFromToken(request.Token);

        var beers = await _beerRepository.GetUserFavoriteBeers(userId, request.Page, request.PageSize);

        if (!beers.Any()) return new List<BeerDto>();
        
        var beerIds = beers.Select(b => b.BeerId).ToArray().Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize);

        var beerDtos = await _beerApiService.GetBeersAsync(request.Page, request.PageSize, beerIds.ToList());

        return beerDtos;
    }
}

public class GetFavouriteBeersQueryValidator : AbstractValidator<GetFavouriteBeersQuery>
{
    public GetFavouriteBeersQueryValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Unauthorised.");
        RuleFor(x => x.PageSize).InclusiveBetween(1, 25).WithMessage("Page size must be between 1 and 25.");
        RuleFor(x => x.Page).GreaterThanOrEqualTo(1).WithMessage("Page must be greater than or equal to 1.");
    }
}