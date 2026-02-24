using System.ComponentModel.DataAnnotations;

namespace FinancialControlAPI.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Descricao { get; set; }  
        
        [Required]
        public decimal Valor { get; set; }  

        [Required]
        [MaxLength(20)]
        public string Tipo { get; set; }    
        public DateTime Data { get; set; }  = DateTime.UtcNow;
        public int UserId { get; set; } 
        public User User { get; set; }
    }
}
