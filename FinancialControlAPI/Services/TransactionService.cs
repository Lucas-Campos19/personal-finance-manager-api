using FinancialControlAPI.Data;
using FinancialControlAPI.DTOs;
using FinancialControlAPI.Entities;
using FinancialControlAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace FinancialControlAPI.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;
        public TransactionService(AppDbContext context)
        {
            _context = context; 
        }
        public async Task<Transaction> Create(CreateTransactionDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!userExists)
                throw new Exception("Usuário não encontrado.");

            var transaction = new Transaction
            {
                Descricao = dto.Descricao,
                Valor = dto.Valor,
                Tipo = dto.Tipo,
                Data = dto.Data,
                UserId = dto.UserId,
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return transaction;
        }

        public async Task<Transaction?> GetById(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            return (transaction);
        }

        public async Task<List<Transaction>> FilterByPeriod(DateTime inicio, DateTime fim)
        {
            var datasFiltro = await _context.Transactions.Where(d => d.Data >= inicio && d.Data <= fim).OrderBy(d => d.Data).ToListAsync();
            return (datasFiltro);
        }

        public async Task<FinancialSummaryDto> GetResumo(DateTime? inicio, DateTime? fim)
        {
            var query = _context.Transactions.AsQueryable();
            if (inicio.HasValue)
            {
                query = query.Where(t => t.Data >= inicio.Value);
            }
            if (fim.HasValue)
            {
                query = query.Where(t => t.Data <= fim.Value);
            }
            var receitas = await query
                .Where(t => t.Tipo == "Receita")
                .SumAsync(t => (decimal?)t.Valor) ?? 0;
            var despesas = await query
                .Where(t => t.Tipo == "Despesa")
                .SumAsync(t => (decimal?)t.Valor) ?? 0;

            var resumo = new FinancialSummaryDto
            {
                TotalReceitas = receitas,
                TotalDespesas = despesas,
            };

            return (resumo);
        }

        public async Task<StatisticsTransactionDto> GetStatistics(DateTime? inicio, DateTime? fim)
        {
            var query = _context.Transactions.AsQueryable();
            if (inicio.HasValue)
            {
                query = query.Where(t => t.Data >= inicio.Value);
            }
            if (fim.HasValue)
            {
                query = query.Where(t => t.Data <= fim.Value);
            }
            var valorMedio = await query.AverageAsync(t => (decimal?)t.Valor) ?? 0;
            var quantidadeTransacoes = await query.CountAsync();
            var maiorValor = await query.MaxAsync(p => (decimal?)p.Valor) ?? 0;
            var menorValor = await query.MinAsync(p => (decimal?)p.Valor) ?? 0;

            var estatistica = new StatisticsTransactionDto
            {
                ValorMedio = valorMedio,
                MaiorValor = maiorValor,
                MenorValor = menorValor,
                TotalTransacoes = quantidadeTransacoes,
            };
            return (estatistica);
        }
    }
}
