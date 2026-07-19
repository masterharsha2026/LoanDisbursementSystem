using LoanDisbursementSystem.DTO;
using LoanDisbursementSystem.Repositories;

namespace LoanDisbursementSystem.Services
{
    public class DisbursementService : IDisbursementService
    {
        private readonly IDisbursementRepository _disbursementRepository;

        public DisbursementService(IDisbursementRepository disbursementRepository)
        {
            _disbursementRepository = disbursementRepository;
        }

        public async Task<List<DisbursementResponseDto>> GetAllAsync()
        {
            return await _disbursementRepository.GetAllAsync();
        }

        public async Task<DisbursementResponseDto?> GetByIDAsync(int id)
        {
            return await _disbursementRepository.GetByIDAsync(id);
        }

        public async Task<ApiResponseDto> DisburseLoanAsync(DisbursementRequestDto disbursementRequestDto)
        {
            return await _disbursementRepository.DisburseLoanAsync(disbursementRequestDto);
        }
    }
}