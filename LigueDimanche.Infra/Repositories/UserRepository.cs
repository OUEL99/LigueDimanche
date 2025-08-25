using LigueDimanche.Core.Entities;
using LigueDimanche.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LigueDimanche.Infra.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<User>> GetUsersByEquipeAsync(int equipeId)
        {
            return await _context.EquipeMembres
                .Where(em => em.EquipeId == equipeId)
                .Select(em => em.User)
                .ToListAsync();
        }
    }
}