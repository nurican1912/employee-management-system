using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync(string? search = null, int? departmentId = null);
        Task<List<Employee>> GetRecentAsync(int count);
        Task<int> GetTotalCountAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
