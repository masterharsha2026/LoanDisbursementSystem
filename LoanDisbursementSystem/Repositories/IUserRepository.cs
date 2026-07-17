using LoanDisbursementSystem.Models;

namespace LoanDisbursementSystem.Repositories
{
    public interface IUserRepository
    {

        Task<User?> GetUserAsync(string username, string password);
    }
}
