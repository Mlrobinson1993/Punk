import type { Beer } from "./Beer";

export interface User {
  id: string;
  name: string;
  username: string;
  favouriteBeers: Beer.Dto[];
  token: string;
  refreshToken: string;
}
