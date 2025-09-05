using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LigueDimanche.Core.Interfaces
{
    public interface IEquipeRepository
    {
        Task<IEnumerable<Entities.Equipe>> GetAllEquipesAsync();

    }
}
