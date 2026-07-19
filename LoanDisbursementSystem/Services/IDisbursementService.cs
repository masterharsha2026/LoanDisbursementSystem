using LoanDisbursementSystem.DTO;

namespace LoanDisbursementSystem.Services
{
    public interface IDisbursementService
    {
        Task<List<DisbursementResponseDto>> GetAllAsync();

        Task<DisbursementResponseDto?> GetByIDAsync(int id);

        Task<ApiResponseDto> DisburseLoanAsync(DisbursementRequestDto disbursementRequestDto);
    }
}