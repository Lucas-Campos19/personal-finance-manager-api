namespace FinancialControlAPI.DTOs
{
    public class FinancialSummaryDto
    {
        public decimal TotalReceitas { get; set; }  
        public decimal TotalDespesas { get; set; }

        public decimal Saldo => TotalReceitas - TotalDespesas;
    }
}
