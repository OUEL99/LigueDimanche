using LigueDimanche.Core.DTO.Users;
using LigueDimanche.Core.Entities;
using LigueDimanche.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace LigueDimanche.Infra.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(MapToResponseDto);
        }

        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user == null ? null : MapToResponseDto(user);
        }

        public async Task<UserResponseDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user == null ? null : MapToResponseDto(user);
        }

        public async Task<UserResponseDto> CreateUserAsync(CreateUserRequestDto request)
        {
            // Vérification si l'email existe déjà
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException($"Un utilisateur avec l'email {request.Email} existe déjà");
            }

            // Validation métier
            if (request.DateDeNaissance > DateTime.Now.AddYears(-15))
            {
                throw new InvalidOperationException("L'utilisateur doit avoir au moins 15 ans");
            }

            // Hashage du mot de passe
            var hashedPassword = HashPassword(request.MotDePasse);

            // Création de l'entité
            var user = new User
            {
                Email = request.Email.ToLower(),
                Nom = request.Nom,
                Prenom = request.Prenom,
                MotDePasse = hashedPassword,
                DateDeNaissance = request.DateDeNaissance,
                Telephone = request.Telephone,
                EstAdmin = request.EstAdmin,
                Positions = MapStringPositionsToEnum(request.Positions)
            };

            var createdUser = await _userRepository.AddAsync(user);
            return MapToResponseDto(createdUser);
        }

        public async Task<UserResponseDto?> UpdateUserAsync(int id, UpdateUserRequestDto request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            // Mise à jour des propriétés
            user.Nom = request.Nom;
            user.Prenom = request.Prenom;
            user.DateDeNaissance = request.DateDeNaissance;
            user.Telephone = request.Telephone;
            user.Positions = MapStringPositionsToEnum(request.Positions);

            await _userRepository.UpdateAsync(user);
            return MapToResponseDto(user);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<UserResponseDto>> GetUsersByEquipeAsync(int equipeId)
        {
            var users = await _userRepository.GetUsersByEquipeAsync(equipeId);
            return users.Select(MapToResponseDto);
        }

        #region Méthodes privées de mapping et utilitaires

        private static UserResponseDto MapToResponseDto(User user)
        {
            return new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Nom = user.Nom,
                Prenom = user.Prenom,
                DateDeNaissance = user.DateDeNaissance,
                Telephone = user.Telephone,
                EstAdmin = user.EstAdmin,
                Positions = user.Positions.Select(p => p.ToString()).ToList()
            };
        }

        private static List<Position> MapStringPositionsToEnum(List<string> positions)
        {
            return positions
                .Where(p => Enum.TryParse<Position>(p, true, out _))
                .Select(p => Enum.Parse<Position>(p, true))
                .ToList();
        }

        private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }

        #endregion
    }
}