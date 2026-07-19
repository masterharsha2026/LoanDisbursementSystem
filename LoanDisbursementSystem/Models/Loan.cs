namespace LoanDisbursementSystem.Models
{
    public class Loan
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal LoanAmount { get; set; }

        public string status { get; set; } = "pending";

        public Customer? customer { get; set; }

        public ICollection<Disbursement> Disbursements { get; set; } = new List<Disbursement>();
    }
}
