using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Joueur
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required DateTime DateNaissance { get; set; }
        public required string Email { get; set; }
        public required string Telephone { get; set; }
        public required bool IsAdmin { get; set; }

        public required string MotDePasseHash { get; set; } = string.Empty;
        public DateTime? LastLogin { get; set; }

        public required bool IsEmailVerified { get; set; } = false;
        public string? EmailVerificationToken { get; set; }
        public DateTime? EmailVerificationTokenExpiry { get; set; }

        public ICollection<JoueurEquipe> JoueurEquipes { get; set; } = [];
        public ICollection<JoueurSaison> JoueurSaisons { get; set; } = [];
        public ICollection<JoueurPosition> JoueurPositions { get; set; } = [];
    }
}
