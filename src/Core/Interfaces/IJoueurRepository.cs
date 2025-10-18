using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
namespace Core.Interfaces
{
    /// <summary>
    /// Définit le contrat pour un dépôt qui gère les entités <see cref="Joueur"/>.
    /// </summary>
    /// <remarks>Cette interface étend <see cref="IRepositoryBase{T}"/> pour fournir des fonctionnalités de dépôt
    /// spécifiques aux entités <see cref="Joueur"/>. Elle peut être utilisée pour effectuer des opérations CRUD et d'autres tâches d'accès aux données
    /// pour les objets <see cref="Joueur"/>.</remarks>
    public interface IJoueurRepository : IRepositoryBase<Joueur>
    {
        /// <summary>
        /// Récupère tous les joueurs associés à une position spécifique.
        /// </summary>
        /// <param name="positionId">ID de la position souhaitée</param>
        /// <returns>Liste des joueurs ayant cette position</returns>
        Task<IEnumerable<Joueur>> GetJoueursByPositionAsync(int positionId);
        Task<Joueur?> GetJoueurByEmailAsync(string email);
        Task<Joueur?> GetJoueurByEmailVerificationToken(string token);
    }
}
