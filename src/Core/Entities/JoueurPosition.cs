using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class JoueurPosition
    {
        public int JoueurId { get; set; }
        public Joueur Joueur { get; set; } = null!;
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
    }
}
