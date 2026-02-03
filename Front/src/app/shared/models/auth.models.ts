export interface LoginRequest {
  userName: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  success: boolean;
  message: string;
}

export interface User {
  id: string;
  email: string;
  name: string;
}
