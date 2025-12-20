using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeManagementContext _context;

        public EmployeeService(EmployeeManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync(string? search = null, int? departmentId = null)
        {
            var query = _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(e =>
                    e.FirstName.Contains(search) ||
                    e.LastName.Contains(search) ||
                    e.Email.Contains(search));

            if (departmentId.HasValue)
                query = query.Where(e => e.DepartmentId == departmentId.Value);

            return await query.OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync();
        }

        public async Task<List<Employee>> GetRecentAsync(int count)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .OrderByDescending(e => e.EmployeeId)
                .Take(count)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Employees.CountAsync();
        }

        public async Task<Employee?> GetByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task CreateAsync(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Employees.AnyAsync(e => e.EmployeeId == id);
        }
    }
}
