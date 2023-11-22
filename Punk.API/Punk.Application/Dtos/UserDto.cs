using System.Text.Json.Serialization;
using Punk.Domain;
using Punk.Domain.Models;

namespace Punk.Application.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }


    public string Token { get; set; }


    public string RefreshToken { get; set; }

    public List<FavouriteBeer> FavouriteBeers { get; set; }
}