namespace LigueDimanche.Core.DTO.Users
{
    public class CreateUserRequestDto
    {
        public required string Email { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public required string MotDePasse { get; set; }
        public required DateTime DateDeNaissance { get; set; }
        public required string Telephone { get; set; }
        public bool EstAdmin { get; set; }
        public List<string> Positions { get; set; } = new List<string>();
    }
}