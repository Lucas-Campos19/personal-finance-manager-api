using System.ComponentModel.DataAnnotations;

namespace FinancialControlAPI.DTOs
{
    public class UserUpdateDto
    {
        [Required]
        [MaxLength(100)]
        public string Nome { get; set; }

        [EmailAddress]
        [Required]
        [MaxLength(150)]
        public string Email { get; set; }
    }
}
