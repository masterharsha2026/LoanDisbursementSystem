import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AUTH_TOKEN_KEY } from './api.config';

export const authGuard: CanActivateFn = () => {
  const token = typeof window !== 'undefined' ? localStorage.getItem(AUTH_TOKEN_KEY) : null;
  const router = inject(Router);

  if (token) {
    return true;
  }

  return router.createUrlTree(['/login']);
};
