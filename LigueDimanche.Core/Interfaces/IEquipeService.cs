using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigueDimanche.Core.DTO.Equipe;

namespace LigueDimanche.Core.Interfaces
{
    public interface IEquipeService
    {
        Task<IEnumerable<EquipeResponseDto>> GetAllEquipesAsync();

    }
}
