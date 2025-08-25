using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigueDimanche.Core.DTO.Users
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public DateTime DateDeNaissance { get; set; }
        public string Telephone { get; set; } = string.Empty;
        public bool EstAdmin { get; set; }
        public List<string> Positions { get; set; } = new List<string>();
    }
}
