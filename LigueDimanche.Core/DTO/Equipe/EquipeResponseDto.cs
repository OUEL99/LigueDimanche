using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigueDimanche.Core.DTO.Equipe
{
    public class EquipeResponseDto
    {
        public int Id { get; set; }
        public string Nom { get; set; } = string.Empty;
        public int MatchId { get; set; }
        public List<Users.UserResponseDto> Joueurs { get; set; } = new List<Users.UserResponseDto>();
    }
}
