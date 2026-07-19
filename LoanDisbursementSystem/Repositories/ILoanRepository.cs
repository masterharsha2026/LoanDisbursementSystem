using LoanDisbursementSystem.DTO;

namespace LoanDisbursementSystem.Repositories
{
    public interface ILoanRepository
    {
        Task<List<LoanResponseDto>> GetAllAsync();

        Task<LoanResponseDto?> GetByIDAsync(int id);

        Task<LoanResponseDto?> CreateAsync(LoanRequestDto loanRequestDto);

        Task<ApiResponseDto> UpdateAsync(int id, LoanUpdateDto loanUpdateDto);

        Task<ApiResponseDto> DeleteAsync(int id);
    }
}