namespace LigueDimanche.Core.Entities
{
    public class EquipeMembre
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int EquipeId { get; set; }
        public Equipe Equipe { get; set; } = null!;
        public RoleEquipe Role { get; set; }
    }

    public enum RoleEquipe
    {
        Joueur,
        Capitaine
    }
}
