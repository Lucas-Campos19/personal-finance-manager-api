using FinancialControlAPI.Data;
using FinancialControlAPI.DTOs;
using FinancialControlAPI.Entities;
using FinancialControlAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinancialControlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;  
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            try
            {
                var transaction = await _transactionService.Create(dto);

                return CreatedAtAction(nameof(GetById),
                    new { id = transaction.Id },
                    transaction);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetById(id);    

            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpGet("periodo")]
        public async Task<IActionResult> FilterByPeriod([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        {
            if (inicio == null || fim == null)
            {
                return BadRequest("As datas são obrigatórias");
            }

            if (inicio > fim)
            {
                return BadRequest("Data inicio não pode ser maior do que a data final");
            }
            var datasFiltro = await _transactionService.FilterByPeriod(inicio, fim);
            return Ok(datasFiltro);

        }

        [HttpGet("resumo")]
        public async Task<IActionResult> GetResumo([FromQuery] DateTime? inicio, [FromQuery] DateTime? fim)
        {
            var resumo = await _transactionService.GetResumo(inicio, fim);
            return Ok(resumo);
        }

        [HttpGet("estatisticas")]
        public async Task<IActionResult> GetStatistics([FromQuery] DateTime ? inicio, [FromQuery] DateTime ? fim)
        {
            var estatistica = await _transactionService.GetStatistics(inicio, fim);
            return Ok(estatistica);
        }
    }
}
