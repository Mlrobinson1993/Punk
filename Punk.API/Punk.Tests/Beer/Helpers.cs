using Punk.Application.Dtos;

namespace Punk.Tests.Beer;

public static class Helpers
{
    public static BeerDto CreateBeer(int id) => new()
    {
        Id = id,
        Name = "Test Beer",
        Tagline = "A Test Beer",
        Description = "This is a test description for a beer.",
        ImageUrl = "https://example.com/test_beer.jpg",
        Abv = 5.0,
        Ibu = 15.0,
        Ingredients = new Ingredients
        {
            Malt = new List<Ingredient>
            {
                new()
                {
                    Name = "Test Malt",
                    Amount = new Volume { Value = 1.0f, Unit = "kg" }
                }
            },
            Hops = new List<Hop>
            {
                new()
                {
                    Name = "Test Hop",
                    Amount = new Volume { Value = 0.5f, Unit = "kg" },
                    Add = "Start",
                    Attribute = "Bitter"
                }
            },
            Yeast = "Test Yeast"
        },
        FoodPairing = new[] { "Test Food 1", "Test Food 2" },
        BrewersTips = "Test Brewer's tips"
    };
}