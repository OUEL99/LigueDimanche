using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IJoueurService
    {
        Task<Joueur> InscrireJoueurAsync(string nom, string prenom, DateTime dateNaissance, string email, string password, string telephone, List<int> positionIds);
    }
}
