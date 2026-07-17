using LoanDisbursementSystem.Data;
using LoanDisbursementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanDisbursementSystem.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserAsync(string username, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username && x.Password == password);
        }
    }
}
