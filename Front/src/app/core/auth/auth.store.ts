import { signalStore, withState, withMethods, patchState } from '@ngrx/signals';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { tapResponse } from '@ngrx/operators';
import { LoginRequest, LoginResponse, User } from '../../shared/models/auth.models';
import { rxMethod } from '@ngrx/signals/rxjs-interop';
import { pipe, switchMap, tap } from 'rxjs';
import { Router } from '@angular/router';

export interface AuthState {
  user: User | null;
  token: string | null;
  isAuthenticated: boolean;
  isLoading: boolean;
  error: string | null;
}

const initialState: AuthState = {
  user: null, // We might want to decode the token to get user info if needed
  token: localStorage.getItem('token'),
  isAuthenticated: !!localStorage.getItem('token'),
  isLoading: false,
  error: null
};

export const AuthStore = signalStore(
  { providedIn: 'root' },
  withState(initialState),
  withMethods((store, authService = inject(AuthService), router = inject(Router)) => ({
    login: rxMethod<LoginRequest>(
      pipe(
        tap(() => patchState(store, { isLoading: true, error: null })),
        switchMap((credentials) =>
          authService.login(credentials).pipe(
            tapResponse({
              next: (response: LoginResponse) => {
                if (response.success) {
                  localStorage.setItem('token', response.token);
                  patchState(store, { 
                    token: response.token, 
                    isAuthenticated: !!response.token, 
                    isLoading: false,
                    user: { id: '0', name: credentials.userName, email: credentials.userName } 
                  });
                  router.navigate(['/dashboard']);
                } else {
                  patchState(store, { isLoading: false, error: response.message });
                }
              },
              error: (err: any) => {
                patchState(store, { 
                  isLoading: false, 
                  error: err.error?.message || 'Login failed' 
                });
              },
            })
          )
        )
      )
    ),
    logout() {
      localStorage.removeItem('token');
      patchState(store, { user: null, token: null, isAuthenticated: false });
      router.navigate(['/auth/login']);
    },
    setLoading(isLoading: boolean) {
      patchState(store, { isLoading });
    },
  }))
);

