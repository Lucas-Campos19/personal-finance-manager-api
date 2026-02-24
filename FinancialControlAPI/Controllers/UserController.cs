using FinancialControlAPI.Data;
using Microsoft.AspNetCore.Mvc;
using FinancialControlAPI.Entities;
using Microsoft.EntityFrameworkCore;
using FinancialControlAPI.DTOs;

namespace FinancialControlAPI.Controllers
{
    [ApiController] // ativa validação automatica de ModelState Binding automático do body resposta 400 automatica se modelo inválido
    [Route("api/[controller]")] //define rota base
    public class UserController : ControllerBase // criação de um controller REST 
    {
       private readonly AppDbContext _context; // injeção de dependência
       public UserController(AppDbContext context) // campo privado para usar o contexto o .net injeta automaticamente por que foi registrado no program.cs
       {
            _context = context;
       }

        [HttpPost]
        public async Task<IActionResult> Create(User user) // criação do usuario, recebe o user no corpo da requisição
        {
            _context.Users.Add(user); // marca como added e salva no banco
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user); // retorna status 201 location header apontando para getByid corpo com objeto criado
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _context.Users.ToListAsync(); // executa SELECT * FROM USERS
            return Ok(users); // retorna 200
        }

        [HttpGet("{id}")] // rota GET api/User/1
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _context.Users.Include(u => u.Transactions).FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                return NotFound();
            }

            var response = new UserResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email, 
                Transactions = user.Transactions?.Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Descricao = t.Descricao,
                    Valor = t.Valor,
                    Tipo = t.Tipo,
                    Data = t.Data
                }).ToList()
            };
            return Ok(response);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if(id != user.Id) // proteção contra inconsistência
            {
                return BadRequest();
            }
            var existingUser = await _context.Users.FindAsync(id); // busca usuario existente

            if(existingUser == null)
            {
                return NotFound();
            }

            existingUser.Nome = user.Nome; // atualiza propriedades manualmente, isso evita sobrescrever campos indesejados
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();

            return NoContent(); // retorna status 204
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id); // busca usuario se não existir retorna 404
            if(user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user); // se existir remove e salva retorna 204
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
