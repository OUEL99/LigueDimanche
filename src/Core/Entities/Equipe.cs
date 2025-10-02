using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Equipe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Couleur { get; set; }

        // Navigation property for related Joueur entities
        public ICollection<JoueurEquipe> JoueurEquipes { get; set; } = new List<JoueurEquipe>();
    }
}
