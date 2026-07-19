using LoanDisbursementSystem.Data;
using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanDisbursementSystem.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoanResponseDto>> GetAllAsync()
        {
            return await _context.Loans
                .Select(l => new LoanResponseDto
                {
                    ID = l.Id,
                    CustomerId = l.CustomerId,
                    LoanAmount = l.LoanAmount,
                    Status = l.status
                })
                .ToListAsync();
        }

        public async Task<LoanResponseDto?> GetByIDAsync(int id)
        {
            return await _context.Loans
                .Where(l => l.Id == id)
                .Select(l => new LoanResponseDto
                {
                    ID = l.Id,
                    CustomerId = l.CustomerId,
                    LoanAmount = l.LoanAmount,
                    Status = l.status
                })
                .FirstOrDefaultAsync();
        }

        public async Task<LoanResponseDto?> CreateAsync(LoanRequestDto loanRequestDto)
        {
            var customerExists = await _context.Customers.AnyAsync(c => c.Id == loanRequestDto.CustomerId);

            if (!customerExists)
            {
                return null;
            }

            var loan = new Loan
            {
                CustomerId = loanRequestDto.CustomerId,
                LoanAmount = loanRequestDto.LoanAmount,
                status = loanRequestDto.Status
            };

            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();

            return new LoanResponseDto
            {
                ID = loan.Id,
                CustomerId = loan.CustomerId,
                LoanAmount = loan.LoanAmount,
                Status = loan.status
            };
        }

        public async Task<ApiResponseDto> UpdateAsync(int id, LoanUpdateDto loanUpdateDto)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);

            if (loan == null)
            {
                return new ApiResponseDto
                {
                    Success = false,
                    message = "Loan not found"
                };
            }

            var customerExists = await _context.Customers.AnyAsync(c => c.Id == loanUpdateDto.CustomerId);

            if (!customerExists)
            {
                return new ApiResponseDto
                {
                    Success = false,
                    message = "Customer not found"
                };
            }

            loan.CustomerId = loanUpdateDto.CustomerId;
            loan.LoanAmount = loanUpdateDto.LoanAmount;
            loan.status = loanUpdateDto.Status;

            await _context.SaveChangesAsync();

            return new ApiResponseDto
            {
                Success = true,
                message = "Loan updated successfully"
            };
        }

        public async Task<ApiResponseDto> DeleteAsync(int id)
        {
            var loan = await _context.Loans.FirstOrDefaultAsync(l => l.Id == id);

            if (loan == null)
            {
                return new ApiResponseDto
                {
                    Success = false,
                    message = "Loan not found"
                };
            }

            _context.Loans.Remove(loan);
            await _context.SaveChangesAsync();

            return new ApiResponseDto
            {
                Success = true,
                message = "Loan deleted successfully"
            };
        }
    }
}