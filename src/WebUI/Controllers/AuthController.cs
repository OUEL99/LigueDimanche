using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace WebUI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IJoueurService joueurService) : ControllerBase
    {
        private readonly IJoueurService _joueurService = joueurService;

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var joueur = await _joueurService.InscrireJoueurAsync(
                    request.Nom,
                    request.Prenom,
                    request.DateNaissance,
                    request.Email,
                    request.Password,
                    request.Telephone,
                    request.PositionIds
                );
                return Ok(new
                {
                    Message = "Inscription réussie. Veuillez vérifier votre email pour activer votre compte.",
                    JoueurId = joueur.Id
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    // Définir le modèle RegisterRequest si nécessaire
    public class RegisterRequest
    {
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string Telephone { get; set; }
        public required List<int> PositionIds { get; set; }
    }
}
