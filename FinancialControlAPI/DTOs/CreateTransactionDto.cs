using System.ComponentModel.DataAnnotations;

namespace FinancialControlAPI.DTOs
{
    public class CreateTransactionDto
    {
        [Required]
        public string Descricao { get; set; }

        [Required] 
        public decimal Valor { get; set; }

        [Required]
        public string Tipo { get; set; }

        [Required]
        public DateTime Data { get; set;}

        [Required]
        public int UserId { get; set; } 
    }
}
