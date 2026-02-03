import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { AuthStore } from '../auth/auth.store';

export const authGuard = () => {
  const store = inject(AuthStore);
  const router = inject(Router);

  if (store.isAuthenticated()) {
    return true;
  }

  router.navigate(['/auth/login']);
  return false;
};
