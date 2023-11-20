interface GetBeerByNameQuery {
  name: string;
}

interface GetBeersQuery {
  ids: number[];
  page: number;
  pageSize: number;
}

interface GetFavouriteBeersQuery {
  page: number;
  pageSize: number;
}

interface GetUserByIdQuery {
  id: string;
}
