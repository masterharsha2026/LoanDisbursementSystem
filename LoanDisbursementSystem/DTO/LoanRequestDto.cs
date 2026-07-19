using System.ComponentModel.DataAnnotations;

namespace LoanDisbursementSystem.DTO
{
    public class LoanRequestDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal LoanAmount { get; set; }

        public string Status { get; set; } = "pending";
    }
}