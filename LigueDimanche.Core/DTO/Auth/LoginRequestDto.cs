using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigueDimanche.Core.DTO.Auth
{
    public class LoginRequestDto
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
    }
}
