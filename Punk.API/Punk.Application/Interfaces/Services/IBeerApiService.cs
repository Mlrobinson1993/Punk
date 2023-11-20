using Punk.Application.Dtos;

namespace Punk.Domain.Interfaces.Services;

public interface IBeerApiService
{
    Task<List<BeerDto>> GetBeersAsync(int page = 1, int perPage = 25, List<int>? ids = null);
    Task<List<BeerDto>> GetBeerAsync(string beerName);
}