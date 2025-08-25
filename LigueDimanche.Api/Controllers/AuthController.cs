using Microsoft.AspNetCore.Mvc;
using LigueDimanche.Core.Interfaces;
using LigueDimanche.Core.DTO.Auth;

namespace LigueDimanche.Api.Controllers
{
    /// <summary>
    /// Gestion de l'authentification
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Connexion utilisateur
        /// </summary>
        /// <param name="request">Identifiants de connexion</param>
        /// <returns>Token et informations de l'utilisateur</returns>
        /// <response code="200">Connexion réussie</response>
        /// <response code="401">Email ou mot de passe incorrect</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponseDto), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        /// <summary>
        /// Validation d'un Token
        /// </summary>
        /// <param name="token">Token à valider</param>
        /// <returns>Résultat de la validation</returns>
        [HttpPost("validate-token")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<bool>> ValidateToken([FromBody] string token)
        {
            var isValid = await _authService.ValidateTokenAsync(token);
            if (!isValid)
                return Unauthorized(new { message = "Token invalide ou expiré" });
            return Ok(true);
        }
    }
}
