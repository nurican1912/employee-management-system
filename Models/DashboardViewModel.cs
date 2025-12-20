namespace EmployeeManagementSystem.Models
{
    public class DashboardViewModel
    {
        public int EmployeeCount { get; set; }
        public int DepartmentCount { get; set; }
        public int PositionCount { get; set; }
        public List<Employee> RecentEmployees { get; set; } = new();
    }
}
