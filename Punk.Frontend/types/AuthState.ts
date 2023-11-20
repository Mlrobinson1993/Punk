export interface AuthState {
  isLoading: boolean;
  signUpActive: boolean;
  error: string;
  loginData: {
    email: string;
    password: string;
    name: string;
  };
}
