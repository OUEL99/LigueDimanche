using LigueDimanche.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigueDimanche.Core.DTO.Equipe;

namespace LigueDimanche.Infra.Services
{
    public class EquipeService : IEquipeService
    {
        private readonly IEquipeRepository _equipeRepository;

        public EquipeService(IEquipeRepository equipeRepository)
        {
            _equipeRepository = equipeRepository;
        }

        public async Task<IEnumerable<EquipeResponseDto>> GetAllEquipesAsync()
        {
            var equipes = await _equipeRepository.GetAllEquipesAsync();
            return equipes.Select(e => new EquipeResponseDto
            {
                Id = e.Id,
                Nom = e.Nom,
                MatchId = e.Match.Id,
                Joueurs = e.Membres.Select(m => new Core.DTO.Users.UserResponseDto
                {
                    Id = m.User.Id,
                    Email = m.User.Email,
                    Nom = m.User.Nom,
                    Prenom = m.User.Prenom,
                    DateDeNaissance = m.User.DateDeNaissance,
                    Telephone = m.User.Telephone,
                    EstAdmin = m.User.EstAdmin,
                    Positions = m.User.Positions.Select(p => p.ToString()).ToList()
                }).ToList()
            });
        }
    }
}
