using FinancialControlAPI.Data;
using Microsoft.AspNetCore.Mvc;
using FinancialControlAPI.Entities;
using Microsoft.EntityFrameworkCore;
using FinancialControlAPI.DTOs;
using FinancialControlAPI.Services;

namespace FinancialControlAPI.Controllers
{
    [ApiController] // ativa validação automatica de ModelState Binding automático do body resposta 400 automatica se modelo inválido
    [Route("api/[controller]")] //define rota base
    public class UserController : ControllerBase // criação de um controller REST 
    {
       private readonly IUserService _userService; // injeção de dependência
       public UserController(IUserService userService) // campo privado para usar o contexto o .net injeta automaticamente por que foi registrado no program.cs
       {
            _userService = userService;
       }

        [HttpPost]
        public async Task<IActionResult> Create(UserCreateDto user) // criação do usuario, recebe o user no corpo da requisição
        {
            var createdUser = await _userService.Create(user);
            return CreatedAtAction(
                nameof(GetById),
                new {id = createdUser.Id},
                createdUser
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAll(); ; // executa SELECT * FROM USERS
            return Ok(users); // retorna 200
        }

        [HttpGet("{id}")] // rota GET api/User/1
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            if(user == null)
            {
                return NotFound("Usuario não encontrado");
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateDto user)
        {
            var existingUser = await _userService.Update(id, user); // busca usuario existente

            if(existingUser == null)
            {
                return NotFound("Usuario não encontrado");
            }
            return NoContent(); // retorna status 204
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var user = await _userService.SoftDelete(id); // busca usuario se não existir retorna 404
            if (user == false)
            {
                return NotFound("Usuario não encontrado");
            }
            return NoContent();
        }
    }
}
