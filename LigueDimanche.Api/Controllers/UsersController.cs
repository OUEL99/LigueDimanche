using LigueDimanche.Core.DTO.Users;
using LigueDimanche.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LigueDimanche.Api.Controllers
{
    /// <summary>
    /// Gestion des utilisateurs de la Ligue du Dimanche
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Récupère la liste de tous les utilisateurs
        /// </summary>
        /// <returns>Liste des utilisateurs</returns>
        /// <response code="200">Retourne la liste des utilisateurs</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), 200)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// Récupère un utilisateur par son ID
        /// </summary>
        /// <param name="id">ID de l'utilisateur</param>
        /// <returns>L'utilisateur correspondant</returns>
        /// <response code="200">Retourne l'utilisateur</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserResponseDto>> GetUser(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound($"Utilisateur avec l'ID {id} introuvable");
            
            return Ok(user);
        }

        /// <summary>
        /// Récupère un utilisateur par son email
        /// </summary>
        /// <param name="email">Email de l'utilisateur</param>
        /// <returns>L'utilisateur correspondant</returns>
        /// <response code="200">Retourne l'utilisateur</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpGet("by-email/{email}")]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserResponseDto>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
                return NotFound($"Utilisateur avec l'email {email} introuvable");
            
            return Ok(user);
        }

        /// <summary>
        /// Récupère tous les utilisateurs d'une équipe
        /// </summary>
        /// <param name="equipeId">ID de l'équipe</param>
        /// <returns>Liste des membres de l'équipe</returns>
        /// <response code="200">Retourne la liste des membres</response>
        [HttpGet("by-equipe/{equipeId}")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), 200)]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsersByEquipe(int equipeId)
        {
            var users = await _userService.GetUsersByEquipeAsync(equipeId);
            return Ok(users);
        }

        /// <summary>
        /// Crée un nouvel utilisateur
        /// </summary>
        /// <param name="request">Données de l'utilisateur à créer</param>
        /// <returns>L'utilisateur créé</returns>
        /// <response code="201">Utilisateur créé avec succès</response>
        /// <response code="400">Données invalides (email déjà utilisé, âge insuffisant, etc.)</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDto), 201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<UserResponseDto>> CreateUser([FromBody] CreateUserRequestDto request)
        {
            try
            {
                var createdUser = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Met à jour un utilisateur existant
        /// </summary>
        /// <param name="id">ID de l'utilisateur à modifier</param>
        /// <param name="request">Nouvelles données de l'utilisateur</param>
        /// <returns>L'utilisateur modifié</returns>
        /// <response code="200">Utilisateur modifié avec succès</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<UserResponseDto>> UpdateUser(int id, [FromBody] UpdateUserRequestDto request)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request);
            if (updatedUser == null)
                return NotFound($"Utilisateur avec l'ID {id} introuvable");
            
            return Ok(updatedUser);
        }

        /// <summary>
        /// Supprime un utilisateur
        /// </summary>
        /// <param name="id">ID de l'utilisateur à supprimer</param>
        /// <response code="204">Utilisateur supprimé avec succès</response>
        /// <response code="404">Utilisateur non trouvé</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            if (!deleted)
                return NotFound($"Utilisateur avec l'ID {id} introuvable");
            
            return NoContent();
        }
    }
}