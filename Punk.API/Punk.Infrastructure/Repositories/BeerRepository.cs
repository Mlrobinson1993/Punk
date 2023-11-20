using Microsoft.EntityFrameworkCore;
using Punk.Domain;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Models;
using Punk.Infrastructure;


namespace Punk.Infrastructure.Repositories;

public class BeerRepository : IBeerRepository
{
    private readonly ApplicationDbContext _context;

    public BeerRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<FavouriteBeer>> GetUserFavoriteBeers(Guid userId, int page = 1, int pageSize = 25)
    {
        var userFavoriteBeers = await _context.FavouriteBeers
            .Where(fb => fb.UserId == userId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();


        return userFavoriteBeers;
    }

    public async Task FavouriteBeer(Guid userId, int beerId)
    {
        var exists = await _context.FavouriteBeers
            .AnyAsync(fb => fb.UserId == userId && fb.BeerId == beerId);

        if (!exists)
        {
            var favouriteBeer = new FavouriteBeer
            {
                UserId = userId,
                BeerId = beerId
            };

            _context.FavouriteBeers.Add(favouriteBeer);
            await _context.SaveChangesAsync();
        }
    }

    public async Task UnfavouriteBeer(Guid userId, int beerId)
    {
        var favouriteBeer = await _context.FavouriteBeers
            .FirstOrDefaultAsync(fb => fb.UserId == userId && fb.BeerId == beerId);

        if (favouriteBeer != null)
        {
            _context.FavouriteBeers.Remove(favouriteBeer);
            await _context.SaveChangesAsync();
        }
    }
}