import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../core/api.config';

interface LoanResponse {
  id: number;
  customerId: number;
  loanAmount: number;
  status: string;
}

interface ApiResponse {
  success: boolean;
  message: string;
}

@Component({
  selector: 'app-loan-list',
  imports: [CommonModule, FormsModule],
  templateUrl: './loan-list.html',
  styleUrl: './loan-list.css',
})
export class LoanList {
  loans: LoanResponse[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';

  formData = {
    id: 0,
    customerId: 1,
    loanAmount: 25000,
    status: 'Approved'
  };

  editMode = false;

  constructor(private readonly http: HttpClient) {
    this.loadLoans();
  }

  loadLoans(): void {
    this.loading = true;
    this.errorMessage = '';

    this.http.get<LoanResponse[]>(`${API_BASE_URL}/api/loan`).subscribe({
      next: (response) => {
        this.loans = response;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.errorMessage = 'Failed to load loans.';
      }
    });
  }

  submitLoan(): void {
    this.errorMessage = '';
    this.successMessage = '';

    const payload = {
      customerId: this.formData.customerId,
      loanAmount: this.formData.loanAmount,
      status: this.formData.status
    };

    if (this.editMode) {
      this.http
        .put<ApiResponse>(`${API_BASE_URL}/api/loan/${this.formData.id}`, payload)
        .subscribe({
          next: () => {
            this.successMessage = 'Loan updated successfully.';
            this.resetForm();
            this.loadLoans();
          },
          error: () => {
            this.errorMessage = 'Failed to update loan.';
          }
        });
      return;
    }

    this.http.post<LoanResponse>(`${API_BASE_URL}/api/loan`, payload).subscribe({
      next: () => {
        this.successMessage = 'Loan created successfully.';
        this.resetForm();
        this.loadLoans();
      },
      error: () => {
        this.errorMessage = 'Failed to create loan.';
      }
    });
  }

  editLoan(loan: LoanResponse): void {
    this.editMode = true;
    this.formData = {
      id: loan.id,
      customerId: loan.customerId,
      loanAmount: loan.loanAmount,
      status: loan.status
    };
  }

  deleteLoan(id: number): void {
    this.errorMessage = '';
    this.successMessage = '';

    this.http.delete<ApiResponse>(`${API_BASE_URL}/api/loan/${id}`).subscribe({
      next: () => {
        this.successMessage = 'Loan deleted successfully.';
        this.loadLoans();
      },
      error: () => {
        this.errorMessage = 'Failed to delete loan.';
      }
    });
  }

  resetForm(): void {
    this.editMode = false;
    this.formData = {
      id: 0,
      customerId: 1,
      loanAmount: 25000,
      status: 'Approved'
    };
  }
}
