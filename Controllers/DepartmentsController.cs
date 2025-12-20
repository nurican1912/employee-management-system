using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _departmentService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var department = await _departmentService.GetByIdAsync(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentId,Name")] Department department)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentService.CreateAsync(department);
                    TempData["SuccessMessage"] = "Departman başarıyla eklendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Departman eklenirken bir hata oluştu.");
                }
            }
            return View(department);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var department = await _departmentService.GetByIdAsync(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DepartmentId,Name")] Department department)
        {
            if (id != department.DepartmentId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _departmentService.UpdateAsync(department);
                    TempData["SuccessMessage"] = "Departman başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _departmentService.ExistsAsync(id)) return NotFound();
                    TempData["ErrorMessage"] = "Departman güncellenirken bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            return View(department);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departmentService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Departman başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
