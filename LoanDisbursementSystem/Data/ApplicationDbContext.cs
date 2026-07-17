using LoanDisbursementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LoanDisbursementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }

        public DbSet<User> Users { get; set; }

    }
}
