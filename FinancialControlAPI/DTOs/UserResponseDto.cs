using System.Transactions;

namespace FinancialControlAPI.DTOs
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }   
        public List<TransactionDto> Transactions { get; set; }
        public bool IsDeleted { get; set; }
    }
}
