using LoanDisbursementSystem.DTO;

namespace LoanDisbursementSystem.Repositories
{
    public interface IDisbursementRepository
    {

        Task<List<DisbursementResponseDto>> GetAllAsync();

        Task<DisbursementResponseDto?> GetByIDAsync(int id);

        Task<ApiResponseDto> DisburseLoanAsync(DisbursementRequestDto disbursementRequestDto);
    }
}
