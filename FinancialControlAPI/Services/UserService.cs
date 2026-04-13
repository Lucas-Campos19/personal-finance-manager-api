using FinancialControlAPI.Data;
using FinancialControlAPI.DTOs;
using FinancialControlAPI.Entities;
using FinancialControlAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinancialControlAPI.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<UserResponseDto> Create(UserCreateDto userDto)
        {
            var user = new User
            {
                Nome = userDto.Nome,
                Email = userDto.Email,
            };
            _context.Users.Add(user);// marca como added e salva no banco
            await _context.SaveChangesAsync();
            return new UserResponseDto
            {
                Id = user.Id,
                Nome = user.Nome,
                Email = user.Email
            };
        }

        public async Task<bool> SoftDelete(int id)
        {
            var user = await _context.Users.FindAsync(id); // busca usuario se não existir retorna 404
            if (user == null)
            {
                throw new NotFoundException("Usuario não encontrado");
            }
            if(user.IsDeleted == true)
            {
                throw new NotFoundException("Usuario não encontrado");
            }
            user.IsDeleted = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserResponseDto>> GetAll()
        {
            var users = await _context.Users.Where(u => u.IsDeleted == false).ToListAsync(); // executa SELECT * FROM USERS;
            var response = users.Select(user => new UserResponseDto
            {
                Id= user.Id,
                Nome= user.Nome,
                Email = user.Email,
            }).ToList();
            return response;
        }

        public async Task<UserResponseDto> GetById(int id)
        {
            var user = await _context.Users.Where(u => u.Id == id && u.IsDeleted == false).Include(u => u.Transactions).FirstOrDefaultAsync(u => u.Id == id);
            if(user == null)
            {
                throw new NotFoundException("Usuario não encontrado");
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
            return (response);
        }

        public async Task<UserResponseDto> Update(int id, UserUpdateDto user)
        {
            var existingUser = await _context.Users.FindAsync(id); // busca usuario existente

            if (existingUser == null)
            {
                throw new NotFoundException("Usuario não encontrado");
            }

            if (existingUser.IsDeleted == true)
            {
                throw new NotFoundException("Usuario não encontrado");
            }

            existingUser.Nome = user.Nome; // atualiza propriedades manualmente, isso evita sobrescrever campos indesejados
            existingUser.Email = user.Email;

            await _context.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = existingUser.Id,
                Nome = existingUser.Nome,
                Email = existingUser.Email,
            };
        }
    }
}
