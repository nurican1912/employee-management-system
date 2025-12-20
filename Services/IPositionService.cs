using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public interface IPositionService
    {
        Task<List<Position>> GetAllAsync();
        Task<Position?> GetByIdAsync(int id);
        Task CreateAsync(Position position);
        Task UpdateAsync(Position position);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
