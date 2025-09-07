using LigueDimanche.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigueDimanche.Core.DTO.Equipe
{
    public class MembreRequestDto
    {
        public int UserId { get; set;}
        public RoleEquipe Role { get; set; } = RoleEquipe.Joueur;
    }

    public class UpdateMembreRequestDto
    {
        public RoleEquipe Role { get; set; } = RoleEquipe.Joueur;
    }
}
