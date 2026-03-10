using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FinancialControlAPI.Entities
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [JsonIgnore]
        public List<Transaction>? Transactions { get; set; }

        public bool IsDeleted { get; set; } 
    }
}
