namespace LoanDisbursementSystem.DTO
{
    public class DisbursementRequestDto
    {
        public int LoanId { get; set; }

        public decimal Amount { get; set; }

        public string Remarks { get; set; } = string.Empty;
        
    }
}
