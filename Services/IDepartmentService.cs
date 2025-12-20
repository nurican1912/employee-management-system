using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task CreateAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetEmployeeCountAsync(int departmentId);
    }
}
