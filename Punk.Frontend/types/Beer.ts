export namespace Beer {
  export interface Dto {
    id: number;
    name: string;
    tagline: string;
    description: string;
    imageUrl: string;
    abv: number;
    ibu?: number;
    ingredients: Ingredients;
    foodPairing: string[];
    brewersTips: string;
  }

  export interface Ingredients {
    malt: Ingredient[];
    hops: Hop[];
    yeast: string;
  }

  interface Ingredient {
    name: string;
    amount: Volume;
  }

  interface Hop {
    name: string;
    amount: Volume;
    add: string;
    attribute: string;
  }

  export interface Volume {
    value: number;
    unit: string;
  }

  interface FavouriteBeer {
    userId: string;
    beerId: number;
  }
}
