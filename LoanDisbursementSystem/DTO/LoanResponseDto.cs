namespace LoanDisbursementSystem.DTO
{
    public class LoanResponseDto
    {
        public int ID { get; set; }

        public int CustomerId { get; set; }

        public decimal LoanAmount { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}