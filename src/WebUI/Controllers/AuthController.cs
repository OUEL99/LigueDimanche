using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Controllers
{
    // ========== API ENDPOINTS ==========
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IJoueurService joueurService, IPositionService positionService) : Controller
    {
        private readonly IJoueurService _joueurService = joueurService;
        private readonly IPositionService _positionService = positionService;

        [HttpPost("registerAPI")]
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

        [HttpGet("verify-emailAPI")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            try
            {
                await _joueurService.VerifiyEmailAsync(token);
                return Ok(new { Message = "Email vérifié avec succès. Vous pouvez maintenant vous connecter." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("Register")]
        public async Task<IActionResult> Register()
        {
            var positions = await _positionService.GetAllPositionsAsync();
            ViewBag.Positions = positions;
            return View(new RegisterViewModel
            {
                Nom = string.Empty,
                Prenom = string.Empty,
                DateNaissance = DateTime.Now.AddYears(-18),
                Email = string.Empty,
                Password = string.Empty,
                Telephone = string.Empty,
                PositionIds = []
            });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {   
            if (!ModelState.IsValid)
            {
                var positions = await _positionService.GetAllPositionsAsync();
                ViewBag["Positions"] = positions;
                return View(model);
            }
            try
            {
                var joueur = await _joueurService.InscrireJoueurAsync(
                    model.Nom,
                    model.Prenom,
                    model.DateNaissance,
                    model.Email,
                    model.Password,
                    model.Telephone,
                    model.PositionIds
                );
                TempData["SuccessMessage"] = "Inscription réussie. Veuillez vérifier votre email pour activer votre compte.";
                return RedirectToAction("Login", "Auth");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var positions = await _positionService.GetAllPositionsAsync();
                ViewBag["Positions"] = positions;
                return View(model);
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

    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Le nom est requis.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le nom doit contenir entre 2 et 50 caractères.")]
        public required string Nom { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le prénom est requis.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Le prénom doit contenir entre 2 et 50 caractères.")]
        public required string Prenom { get; set; } = string.Empty;

        [Required(ErrorMessage = "La date de naissance est requise.")]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; } = DateTime.Now.AddYears(-18);

        [Required(ErrorMessage = "L'adresse email est requise.")]
        [EmailAddress(ErrorMessage = "L'adresse email n'est pas valide.")]
        public required string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public required string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
        [Phone(ErrorMessage = "Le numéro de téléphone n'est pas valide.")]
        public required string Telephone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Au moins une position doit être sélectionnée.")]
        public required List<int> PositionIds { get; set; } = [];
    }
}
