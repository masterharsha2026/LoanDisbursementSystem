import { Routes } from '@angular/router';
import { Dashboard } from './components/dashboard/dashboard';
import { Login } from './components/login/login';
import { authGuard } from './core/auth.guard';

export const routes: Routes = [
	{ path: '', redirectTo: 'login', pathMatch: 'full' },
	{ path: 'login', component: Login },
	{ path: 'dashboard', component: Dashboard, canActivate: [authGuard] },
	{ path: '**', redirectTo: 'login' }
];
