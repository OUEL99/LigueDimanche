using Microsoft.AspNetCore.Mvc;
using LigueDimanche.Core.Interfaces;
using LigueDimanche.Core.DTO.Equipe;

namespace LigueDimanche.Api.Controllers
{
    /// <summary>
    /// Gestion des équipes
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EquipesController(IEquipeService equipeService) : ControllerBase
    {
        private readonly IEquipeService _equipeService = equipeService;

        /// <summary>
        /// Récupérer toutes les équipes
        /// </summary>
        /// <returns>Détails de toutes les équipes</returns>
        /// <response code="200">Liste des équipes récupérée avec succès</response>
        /// <response code="500">Erreur serveur</response>
        /// <response code="404">Aucune équipe trouvée</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<EquipeResponseDto>), 200)]
        [ProducesResponseType(500)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<IEnumerable<EquipeResponseDto>>> GetAllEquipes()
        {
            var equipes = await _equipeService.GetAllEquipesAsync();
            if (equipes == null || !equipes.Any())
                return NotFound(new { message = "Aucune équipe trouvée" });
            return Ok(equipes);
        }

        /// <summary>
        /// Récupérer une équipe par son ID
        /// </summary>
        /// <param name="idEquipe">ID de l'équipe recherchée</param>
        /// <response code="200">Équipe récupérée avec succès</response>
        /// <response code="404">Équipe non trouvée</response>
        /// <response code="500">Erreur serveur</response>
        [HttpGet("{idEquipe}")]
        [ProducesResponseType(typeof(EquipeResponseDto), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<EquipeResponseDto>> GetEquipeById(int idEquipe)
        {
            var equipe = await _equipeService.GetEquipeByIdAsync(idEquipe);
            if (equipe == null)
                return NotFound(new { message = $"Équipe avec l'ID {idEquipe} introuvable" });
            return Ok(equipe);
        }

        /// <summary>
        /// Récupérer les 
    }
}
