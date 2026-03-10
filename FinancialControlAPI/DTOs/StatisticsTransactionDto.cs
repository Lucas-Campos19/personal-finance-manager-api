namespace FinancialControlAPI.DTOs
{
    public class StatisticsTransactionDto
    {
        public decimal ValorMedio { get; set; }
        public decimal MaiorValor { get; set; }
        public decimal MenorValor { get; set; }
        public int TotalTransacoes { get; set; }    


    }
}
