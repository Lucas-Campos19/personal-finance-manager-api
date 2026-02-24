namespace FinancialControlAPI.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; } 
        public string Descricao { get; set; }   
        public decimal Valor { get; set; }  
        public string Tipo { get; set; }    
        public DateTime Data { get; set; }
    }
}
