using Microsoft.Graph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class JoueurEquipe
    {
        public int JoueurId { get; set; }
        public Joueur Joueur { get; set; } = null!;
        public int EquipeId { get; set; }
        public Equipe Equipe { get; set; } = null!;
        public bool IsCapitaine { get; set; }
    }
}
