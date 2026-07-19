import { HttpInterceptorFn } from '@angular/common/http';
import { AUTH_TOKEN_KEY } from './api.config';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = typeof window !== 'undefined' ? localStorage.getItem(AUTH_TOKEN_KEY) : null;

  if (!token || req.url.includes('/api/auth/login')) {
    return next(req);
  }

  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`
    }
  });

  return next(authReq);
};
