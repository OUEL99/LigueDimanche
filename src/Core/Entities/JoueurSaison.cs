using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class JoueurSaison
    {
        public int JoueurId { get; set; }
        public Joueur Joueur { get; set; } = null!;
        public int SaisonId { get; set; }
        public Saison Saison { get; set; } = null!;
        public bool EstTempsPlein { get; set; }
    }
}
