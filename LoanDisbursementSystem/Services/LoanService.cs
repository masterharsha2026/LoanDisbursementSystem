using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Repositories;

namespace LoanDisbursementSystem.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;

        public LoanService(ILoanRepository loanRepository)
        {
            _loanRepository = loanRepository;
        }

        public async Task<List<LoanResponseDto>> GetAllAsync()
        {
            return await _loanRepository.GetAllAsync();
        }

        public async Task<LoanResponseDto?> GetByIDAsync(int id)
        {
            return await _loanRepository.GetByIDAsync(id);
        }

        public async Task<LoanResponseDto?> CreateAsync(LoanRequestDto loanRequestDto)
        {
            return await _loanRepository.CreateAsync(loanRequestDto);
        }

        public async Task<ApiResponseDto> UpdateAsync(int id, LoanUpdateDto loanUpdateDto)
        {
            return await _loanRepository.UpdateAsync(id, loanUpdateDto);
        }

        public async Task<ApiResponseDto> DeleteAsync(int id)
        {
            return await _loanRepository.DeleteAsync(id);
        }
    }
}