namespace LigueDimanche.Core.Entities
{
    public class Equipe
    {
        public int Id { get; set; }
        public required string Nom { get; set; }
        public required Match Match { get; set; } = null!;
        public ICollection<EquipeMembre> Membres { get; set; } = new List<EquipeMembre>();
    }
}
