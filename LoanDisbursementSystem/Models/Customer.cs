using System.ComponentModel.DataAnnotations;

namespace LoanDisbursementSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string Mobile { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public ICollection<Loan> Loans { get; set; } = new List<Loan>();
    }
}
