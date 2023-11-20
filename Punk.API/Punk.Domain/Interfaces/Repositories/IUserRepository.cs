using Punk.Domain.Models;

namespace Punk.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> CreateAsync(User user);
    Task<bool> UpdateAsync(User user);
    Task<List<User>> GetUserByFavouriteBeer(int beerId);
}