import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { API_BASE_URL, AUTH_TOKEN_KEY } from '../../core/api.config';

interface LoginResponse {
  token: string;
  userName: string;
  role: string;
}

@Component({
  selector: 'app-login',
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  credentials = {
    userName: 'admin',
    password: 'admin123'
  };

  loading = false;
  errorMessage = '';

  constructor(
    private readonly http: HttpClient,
    private readonly router: Router
  ) {
    if (typeof window !== 'undefined' && localStorage.getItem(AUTH_TOKEN_KEY)) {
      void this.router.navigate(['/dashboard']);
    }
  }

  onLogin(): void {
    this.loading = true;
    this.errorMessage = '';

    this.http
      .post<LoginResponse>(`${API_BASE_URL}/api/auth/login`, this.credentials)
      .subscribe({
        next: (response) => {
          if (typeof window !== 'undefined') {
            localStorage.setItem(AUTH_TOKEN_KEY, response.token);
            localStorage.setItem('loan_disbursement_user', response.userName);
          }
          this.loading = false;
          void this.router.navigate(['/dashboard']);
        },
        error: () => {
          this.loading = false;
          this.errorMessage = 'Invalid username or password.';
        }
      });
  }
}
