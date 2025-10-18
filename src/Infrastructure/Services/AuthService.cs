using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace Infrastructure.Services
{
    public class AuthService(IConfiguration configuration, IJoueurRepository joueurRepository) : IAuthService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IJoueurRepository _joueurRepository = joueurRepository;

        public async Task<string> HashPasswordAsync(string password)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12)));
        }

        public async Task<bool> VerifyPasswordAsync(string hashedPassword, string providedPassword)
        {
            return await Task.FromResult(BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword));
        }

        public async Task<bool> AuthenticateJoueurAsync(string email, string password)
        {
            var joueur = await _joueurRepository.GetJoueurByEmailAsync(email);
            if (joueur == null)
                return false;
            return await VerifyPasswordAsync(joueur.MotDePasseHash, password);
        }

        public async Task<string> GenerateJwtTokenAsync(int joueurId, bool isAdmin)
        {
            var joueur = await _joueurRepository.GetByIdAsync(joueurId);
            if (joueur == null)
                throw new ArgumentException("Joueur not found");
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWT:Key"] ?? throw new InvalidOperationException("JWT Key not configured"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, joueur.Id.ToString()),
                    new Claim(ClaimTypes.Email, joueur.Email),
                    new Claim(ClaimTypes.Role, isAdmin ? "Admin" : "User")
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<string> GenerateEmailVerificationTokenAsync()
        {
            var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            return await Task.FromResult(token);
        }

        public async Task<bool> ValidateEmailVerificationTokenAsync(string token)
        {
            var joueur = await _joueurRepository.GetJoueurByEmailVerificationToken(token);
            if (joueur == null || joueur.EmailVerificationTokenExpiry < DateTime.UtcNow)
                return false;
            joueur.IsEmailVerified = true;
            joueur.EmailVerificationToken = null;
            joueur.EmailVerificationTokenExpiry = null;
            await _joueurRepository.UpdateAsync(joueur);
            return true;
        }

        public async Task<bool> EmailAlreadyUsed(string email)
        {
            var joueur = await _joueurRepository.GetJoueurByEmailAsync(email);
            return joueur != null;
        }
    }
}
