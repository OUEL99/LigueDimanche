namespace LigueDimanche.Core.DTO.Users
{
    public class UpdateUserRequestDto
    {
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required DateTime DateDeNaissance { get; set; }
        public required string Telephone { get; set; }
        public List<string> Positions { get; set; } = new List<string>();
        // Pas d'Email (pas modifiable) ni de MotDePasse (endpoint séparé)
    }
}