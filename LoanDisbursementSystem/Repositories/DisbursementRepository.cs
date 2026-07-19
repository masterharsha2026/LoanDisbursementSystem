using LoanDisbursementSystem.Data;
using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanDisbursementSystem.Repositories
{

    public class DisbursementRepository : IDisbursementRepository
    {
        private ApplicationDbContext _context;

        public DisbursementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDto> DisburseLoanAsync(DisbursementRequestDto disbursementRequestDto)
        {
            // check if loan exist 

            var loan = await _context.Loans.FirstOrDefaultAsync(x => x.Id == disbursementRequestDto.LoanId);

            if(loan == null)
            {
                return new ApiResponseDto
                {
                    Success = false,
                    message = "Loan is not found"
                };
            }

            // create dibursement 
            var dis = new Disbursement
            {
                LoanId = disbursementRequestDto.LoanId,
                Amount = disbursementRequestDto.Amount,
                Remarks = disbursementRequestDto.Remarks,
                DisbursementDate = DateTime.Now
            };

            await _context.Disbursements.AddAsync(dis);

            loan.status = "Disbursed";
            await _context.SaveChangesAsync();
            return new ApiResponseDto
            {
                Success = true,
                message = "Loan Disbursed succeessfully"
            };
            


            // Update loan status 
        }

        public async Task<List<DisbursementResponseDto>> GetAllAsync()
        {
            return await _context.Disbursements.Select(d => new DisbursementResponseDto
            {
                ID = d.Id,
                LoanId = d.LoanId,
                Amount = d.Amount,
                Remarks = d.Remarks,
                DisbursementDate = d.DisbursementDate
            }).ToListAsync();
        }

        public async Task<DisbursementResponseDto?> GetByIDAsync(int id)
        {
            return await _context.Disbursements.
                Where(d => d.Id == id)
                .Select(d => new DisbursementResponseDto
            {
                ID = d.Id,
                LoanId = d.LoanId,
                Amount = d.Amount,
                Remarks = d.Remarks,
                DisbursementDate = d.DisbursementDate
            }).FirstOrDefaultAsync();
        }
    }
}
