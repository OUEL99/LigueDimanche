namespace LigueDimanche.Core.Entities
{
    public class SaisonJoueur
    {
        public int SaisonId { get; set; }
        public int UserId { get; set; }
        public bool EstTempsPlein { get; set; }

        public Saison Saison { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}