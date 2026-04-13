using FinancialControlAPI.DTOs;
using FinancialControlAPI.Entities;

namespace FinancialControlAPI.Services
{
    public interface IUserService
    {
        Task<UserResponseDto> Create(UserCreateDto user);
        Task<List<UserResponseDto>> GetAll();
        Task<UserResponseDto> GetById(int id);
        Task<UserResponseDto> Update(int id, UserUpdateDto user);
        Task<bool> SoftDelete(int id);
    }
}
