using LigueDimanche.Core.Entities;

namespace LigueDimanche.Core.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetUsersByEquipeAsync(int equipeId);
    }
}