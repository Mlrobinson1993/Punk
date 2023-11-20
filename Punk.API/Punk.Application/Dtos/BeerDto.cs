using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Punk.Application.Dtos;

public class BeerDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Tagline { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public double Abv { get; set; }
    public double? Ibu { get; set; }
    public Ingredients Ingredients { get; set; }
    [JsonProperty("food_pairing")] public string[] FoodPairing { get; set; }
    [JsonProperty("brewers_tips")] public string BrewersTips { get; set; }
}

public class Ingredients
{
    public List<Ingredient> Malt { get; set; }
    public List<Hop> Hops { get; set; }
    public string Yeast { get; set; }
}

public class Ingredient
{
    public string Name { get; set; }
    public Volume Amount { get; set; }
}

public class Hop
{
    public string Name { get; set; }
    public Volume Amount { get; set; }
    public string Add { get; set; }
    public string Attribute { get; set; }
}

public class Volume
{
    public float Value { get; set; }
    public string Unit { get; set; }
}