using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Entities;

namespace Infrastructure.Services
{
    public class PositionService(IPositionRepository positionRepository) : IPositionService
    {
        private readonly IPositionRepository _positionRepository = positionRepository;

        public async Task<IEnumerable<Position>> GetAllPositionsAsync()
        {
            return await _positionRepository.GetAllAsync();
        }

        public async Task<Position?> GetPositionByIdAsync(int id)
        {
            return await _positionRepository.GetByIdAsync(id);
        }

        public async Task<Position?> GetPositionByNameAsync(string name)
        {
            var positions = await _positionRepository.GetAllAsync();
            return positions.FirstOrDefault(p => p.Nom.Equals(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
