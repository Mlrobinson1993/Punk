using Microsoft.EntityFrameworkCore;
using Punk.Common.Exceptions;
using Punk.Domain;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Models;

namespace Punk.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.FindAsync<User>(id);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task<bool> CreateAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
        if (existingUser != null)
            throw new BadRequestException("User already exists with this email address");

        await _context.Users.AddAsync(user);
        var success = await _context.SaveChangesAsync();

        return success == 1;
    }

    public async Task<bool> UpdateAsync(User user)
    {
        _context.Users.Update(user);
        var success = await _context.SaveChangesAsync();

        return success == 1;
    }

    public async Task<List<User>> GetUserByFavouriteBeer(int beerId)
    {
        var users = await _context.FavouriteBeers
            .Where(fb => fb.BeerId == beerId)
            .Select(fb => fb.User)
            .ToListAsync();

        return users;
    }
}