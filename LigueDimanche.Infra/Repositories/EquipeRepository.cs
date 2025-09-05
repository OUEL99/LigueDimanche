using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LigueDimanche.Core.Interfaces;
using LigueDimanche.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace LigueDimanche.Infra.Repositories
{
    public class EquipeRepository(AppDbContext context) : Repository<Equipe>(context), IEquipeRepository
    {
        public async Task<IEnumerable<Equipe>> GetAllEquipesAsync()
        {
            return await _dbSet
                .Include(e => e.Match)
                .Include(e => e.Membres)
                    .ThenInclude(em => em.User)
                .ToListAsync();
        }
    }
}
