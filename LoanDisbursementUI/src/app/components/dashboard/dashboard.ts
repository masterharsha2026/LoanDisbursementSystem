import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LoanList } from '../loan-list/loan-list';
import { Disbursement } from '../disbursement/disbursement';
import { AUTH_TOKEN_KEY } from '../../core/api.config';

@Component({
  selector: 'app-dashboard',
  imports: [LoanList, Disbursement],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard {
  userName = typeof window !== 'undefined'
    ? localStorage.getItem('loan_disbursement_user') ?? 'User'
    : 'User';

  constructor(private readonly router: Router) {}

  logout(): void {
    if (typeof window !== 'undefined') {
      localStorage.removeItem(AUTH_TOKEN_KEY);
      localStorage.removeItem('loan_disbursement_user');
    }
    void this.router.navigate(['/login']);
  }
}
