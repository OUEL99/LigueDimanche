using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
namespace Core.Interfaces
{
    /// <summary>
    /// Methodes de service pour gérer les positions des joueurs.
    /// </summary>
    public interface IPositionService
    {
        Task<IEnumerable<Position>> GetAllPositionsAsync();
        Task<Position?> GetPositionByIdAsync(int id);
        Task<Position?> GetPositionByNameAsync(string name);
    }
}
