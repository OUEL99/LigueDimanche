namespace LigueDimanche.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string MotDePasse { get; set; }
        public required DateTime DateDeNaissance { get; set; }
        public required string Telephone { get; set; }
        public bool EstAdmin { get; set; }

        public ICollection<EquipeMembre> Equipes { get; set; } = new List<EquipeMembre>();
        public ICollection<SaisonJoueur> Saisons { get; set; } = new List<SaisonJoueur>();
        public List<Position> Positions { get; set; } = new List<Position>();
    }

    public enum Position
    {
        Gardien,
        Defenseur,
        Attaquant
    }
}