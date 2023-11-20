using Punk.Domain;
using Punk.Domain.Models;

namespace Punk.Domain.Interfaces.Repositories;

public interface IBeerRepository
{
    Task<List<FavouriteBeer>> GetUserFavoriteBeers(Guid userId, int page = 1, int pageSize = 25);
    Task FavouriteBeer(Guid userId, int beerId);
    Task UnfavouriteBeer(Guid userId, int beerId);
}