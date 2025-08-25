using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigueDimanche.Core.DTO.Auth;

namespace LigueDimanche.Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
        Task<bool> ValidateTokenAsync(string token);
        string GenerateToken(int userId, string email, bool isAdmin);
    }
}
