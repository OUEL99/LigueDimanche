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
    }
}
