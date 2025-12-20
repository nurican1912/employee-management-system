using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly IPositionService _positionService;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IPositionService positionService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _positionService = positionService;
        }

        public async Task<IActionResult> Index(string? search, int? departmentId)
        {
            var employees = await _employeeService.GetAllAsync(search, departmentId);
            var departments = await _departmentService.GetAllAsync();

            ViewBag.Search = search;
            ViewBag.DepartmentId = departmentId;
            ViewBag.Departments = new SelectList(departments, "DepartmentId", "Name", departmentId);

            return View(employees);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();
            return View(employee);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Phone,BirthDate,Salary,DepartmentId,PositionId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.CreateAsync(employee);
                    TempData["SuccessMessage"] = "Çalışan başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    TempData["ErrorMessage"] = "Çalışan eklenirken bir hata oluştu.";
                }
            }
            await PopulateDropdownsAsync(employee.DepartmentId, employee.PositionId);
            return View(employee);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var employee = await _employeeService.GetByIdAsync(id.Value);
            if (employee == null) return NotFound();
            await PopulateDropdownsAsync(employee.DepartmentId, employee.PositionId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Email,Phone,BirthDate,Salary,DepartmentId,PositionId")] Employee employee)
        {
            if (id != employee.EmployeeId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _employeeService.UpdateAsync(employee);
                    TempData["SuccessMessage"] = "Çalışan başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _employeeService.ExistsAsync(id)) return NotFound();
                    TempData["ErrorMessage"] = "Çalışan güncellenirken bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            await PopulateDropdownsAsync(employee.DepartmentId, employee.PositionId);
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Çalışan başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync(int? selectedDepartment = null, int? selectedPosition = null)
        {
            var departments = await _departmentService.GetAllAsync();
            var positions = await _positionService.GetAllAsync();
            ViewData["DepartmentId"] = new SelectList(departments, "DepartmentId", "Name", selectedDepartment);
            ViewData["PositionId"] = new SelectList(positions, "PositionId", "Name", selectedPosition);
        }
    }
}
