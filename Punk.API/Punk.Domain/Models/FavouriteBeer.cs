namespace Punk.Domain.Models;

public class FavouriteBeer
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int BeerId { get; set; }
}