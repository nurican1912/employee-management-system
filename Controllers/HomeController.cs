using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IPositionService _positionService;

        public HomeController(ILogger<HomeController> logger, IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _departmentService = departmentService;
            _positionService = positionService;
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity!.IsAuthenticated)
                return View("Landing");

            var vm = new DashboardViewModel
            {
                EmployeeCount = await _employeeService.GetTotalCountAsync(),
                DepartmentCount = (await _departmentService.GetAllAsync()).Count,
                PositionCount = (await _positionService.GetAllAsync()).Count,
                RecentEmployees = await _employeeService.GetRecentAsync(5)
            };
            return View(vm);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
