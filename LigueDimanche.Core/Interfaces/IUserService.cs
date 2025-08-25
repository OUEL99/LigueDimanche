using LigueDimanche.Core.DTO.Users;

namespace LigueDimanche.Core.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto?> GetUserByEmailAsync(string email);
        Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request);
        Task<UserResponseDto?> UpdateUserAsync(int id, UpdateUserRequestDto request);
        Task<bool> DeleteUserAsync(int id);
        Task<IEnumerable<UserResponseDto>> GetUsersByEquipeAsync(int equipeId);
    }
}