namespace LoanDisbursementSystem.DTO
{
    public class DisbursementResponseDto
    {
        public int ID { get; set; }

        public int LoanId { get; set; }

        public decimal Amount { get; set; }

        public string Remarks { get; set; } = string.Empty;

        public DateTime DisbursementDate { get; set; }



    }
}
