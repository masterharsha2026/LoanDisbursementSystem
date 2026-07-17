using LoanDisbursementSystem.DTO;

namespace LoanDisbursementSystem.Services
{
    public interface IAuthService
    {

        Task<LoginResponseDto?> LoginAsync(LoginRequestDto reqest);
    }
}
