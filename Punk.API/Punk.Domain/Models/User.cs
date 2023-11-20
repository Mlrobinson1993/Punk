using Punk.Domain;

namespace Punk.Domain.Models;

public class User
{
    public Guid Id { get; set; } // Primary key
    public string Username { get; set; }
    public string Name { get; set; }

    public string PasswordHash { get; set; }
    public byte[] Salt { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime RefreshTokenExpiryTime { get; set; }

    public ICollection<FavouriteBeer> FavouriteBeers { get; set; }
}