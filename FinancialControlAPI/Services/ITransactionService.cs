using FinancialControlAPI.DTOs;
using FinancialControlAPI.Entities;

namespace FinancialControlAPI.Services
{
    public interface ITransactionService
    {
        Task <StatisticsTransactionDto> GetStatistics(DateTime? inicio, DateTime? fim);
        Task<FinancialSummaryDto> GetResumo(DateTime? inicio, DateTime? fim);
        Task<List<Transaction>> FilterByPeriod(DateTime inicio, DateTime fim);
        Task<Transaction?> GetById(int id);
        Task<Transaction> Create(CreateTransactionDto dto);

    }
}
