using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Saison
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Annee { get; set; }
        public required DateTime DateDebut { get; set; }
        public required DateTime DateFin { get; set; }
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal PrixParJoueur { get; set; }

        // Navigation property for related Match and Joueurs entities
        public ICollection<Match> Matches { get; set; } = [];
        public ICollection<JoueurSaison> JoueurSaisons { get; set; } = [];
    }
}
