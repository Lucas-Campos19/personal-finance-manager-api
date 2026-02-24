using FinancialControlAPI.Data;
using FinancialControlAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinancialControlAPI.Entities;

namespace FinancialControlAPI.Controllers
{
    [ApiController]
    [Route("api/[contoller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TransactionController(AppDbContext context)
        {
            _context = context;
            
        }

        [HttpPost]
        public async Task <IActionResult> Create(CreateTransactionDto dto)
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == dto.UserId);

            if (!userExists)
            {
                return BadRequest("Usuário não encontrado.");
            }

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

            return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _context.Transactions
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if(transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction); 
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> FilterByPeriod([FromQuery] DateTime? inicio, [FromQuery]DateTime? fim)
        {
            if(inicio == null || fim == null)
            {
                return BadRequest("As datas são obrigatórias");
            }

            if(inicio > fim)
            {
                return BadRequest("Data inicio não pode ser maior do que a data final");
            }

            var datasFiltro = await _context.Transactions.Where(d => d.Data >= inicio.Value && d.Data <= fim.Value).OrderBy(d => d.Data).ToListAsync();
            return Ok(datasFiltro);
            
        }
    }
}
