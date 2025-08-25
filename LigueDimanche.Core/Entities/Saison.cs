namespace LigueDimanche.Core.Entities
{
    public class Saison
    {
        public int Id { get; set; }
        public required DateTime DateDebut { get; set; }
        public required DateTime DateFin { get; set; }

        public ICollection<SaisonJoueur> Joueurs { get; set; } = new List<SaisonJoueur>();
    }
}
