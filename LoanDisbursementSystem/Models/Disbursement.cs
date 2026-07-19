namespace LoanDisbursementSystem.Models
{
    public class Disbursement
    {
       public int Id { get; set; }

        public int LoanId { get; set; }

        public decimal Amount { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public DateTime DisbursementDate { get; set; }

        public Loan? Loan { get; set; }
    }
}
