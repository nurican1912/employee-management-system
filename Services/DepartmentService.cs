using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly EmployeeManagementContext _context;

        public DepartmentService(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _context.Departments.OrderBy(d => d.Name).ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _context.Departments.FirstOrDefaultAsync(d => d.DepartmentId == id);
        }

        public async Task CreateAsync(Department department)
        {
            _context.Add(department);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _context.Update(department);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Departments.AnyAsync(d => d.DepartmentId == id);
        }

        public async Task<int> GetEmployeeCountAsync(int departmentId)
        {
            return await _context.Employees.CountAsync(e => e.DepartmentId == departmentId);
        }
    }
}
