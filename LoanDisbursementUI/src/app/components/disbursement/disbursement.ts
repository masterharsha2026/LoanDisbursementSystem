import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { API_BASE_URL } from '../../core/api.config';

interface DisbursementResponse {
  id: number;
  loanId: number;
  amount: number;
  remarks: string;
  disbursementDate: string;
}

interface ApiResponse {
  success: boolean;
  message: string;
}

@Component({
  selector: 'app-disbursement',
  imports: [CommonModule, FormsModule],
  templateUrl: './disbursement.html',
  styleUrl: './disbursement.css',
})
export class Disbursement {
  disbursements: DisbursementResponse[] = [];
  loading = false;
  errorMessage = '';
  successMessage = '';

  formData = {
    loanId: 1,
    amount: 10000,
    remarks: 'First disbursement'
  };

  constructor(private readonly http: HttpClient) {
    this.loadDisbursements();
  }

  loadDisbursements(): void {
    this.loading = true;
    this.errorMessage = '';

    this.http
      .get<DisbursementResponse[]>(`${API_BASE_URL}/api/disbursement`)
      .subscribe({
        next: (response) => {
          this.disbursements = response;
          this.loading = false;
        },
        error: () => {
          this.loading = false;
          this.errorMessage = 'Failed to load disbursements.';
        }
      });
  }

  createDisbursement(): void {
    this.errorMessage = '';
    this.successMessage = '';

    this.http
      .post<ApiResponse>(`${API_BASE_URL}/api/disbursement`, this.formData)
      .subscribe({
        next: () => {
          this.successMessage = 'Disbursement added successfully.';
          this.loadDisbursements();
        },
        error: () => {
          this.errorMessage = 'Failed to create disbursement. Ensure loan exists.';
        }
      });
  }
}
