using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class JoueurRepository : RepositoryBase<Joueur>, IJoueurRepository
    {
        public JoueurRepository(LocalDbContext context) : base(context){ }

        public async Task<IEnumerable<Joueur>> GetJoueursByPositionAsync(int positionId)
        {
            return await _dbSet
                .Include(j => j.JoueurPositions)
                .Where(j => j.JoueurPositions.Any(jp => jp.PositionId == positionId))
                .ToListAsync();
        }

        public async Task<Joueur?> GetJoueurByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(j => j.Email == email);
        }

        public async Task<Joueur?> GetJoueurByEmailVerificationToken(string token)
        {
            return await _dbSet.FirstOrDefaultAsync(j => j.EmailVerificationToken == token);
        }
    }
}
