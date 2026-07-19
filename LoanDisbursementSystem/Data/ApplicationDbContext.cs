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

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Disbursement> Disbursements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.customer)
                .WithMany(c => c.Loans)
                .HasForeignKey(l => l.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Loan>()
                .Property(l => l.LoanAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Disbursement>()
                .HasOne(d => d.Loan)
                .WithMany(l => l.Disbursements)
                .HasForeignKey(d => d.LoanId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Disbursement>()
                .Property(d => d.Amount)
                .HasPrecision(18, 2);
        }

    }
}
