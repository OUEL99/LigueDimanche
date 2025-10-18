using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string hashedPassword, string providedPassword);
        Task<bool> AuthenticateJoueurAsync(string email, string password);
        Task<string> GenerateJwtTokenAsync(int joueurId, bool isAdmin);

        Task<string> GenerateEmailVerificationTokenAsync();
        Task<bool> ValidateEmailVerificationTokenAsync(string token);
        Task <bool> EmailAlreadyUsed(string email);
    }
}
