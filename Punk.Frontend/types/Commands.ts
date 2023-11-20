export interface CreateUserCommand {
  name: string;
  email: string;
  password: string;
}

export interface AuthenticateUserCommand {
  email: string;
  password: string;
}

export interface FavouriteBeerCommand {
  userId: string;
  beerId: number;
  isFavourite: boolean;
}
