using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class PositionService : IPositionService
    {
        private readonly EmployeeManagementContext _context;

        public PositionService(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Position>> GetAllAsync()
        {
            return await _context.Positions.OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Position?> GetByIdAsync(int id)
        {
            return await _context.Positions.FirstOrDefaultAsync(p => p.PositionId == id);
        }

        public async Task CreateAsync(Position position)
        {
            _context.Add(position);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Position position)
        {
            _context.Update(position);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var position = await _context.Positions.FindAsync(id);
            if (position != null)
            {
                _context.Positions.Remove(position);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Positions.AnyAsync(p => p.PositionId == id);
        }
    }
}
