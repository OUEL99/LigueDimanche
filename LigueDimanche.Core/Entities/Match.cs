namespace LigueDimanche.Core.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public int SaisonId { get; set; }
        public Saison Saison { get; set; } = null!;

        public int Equipe1Id { get; set; }
        public Equipe Equipe1 { get; set; } = null!;
        
        public int Equipe2Id { get; set; }
        public Equipe Equipe2 { get; set; } = null!;
    }
}
