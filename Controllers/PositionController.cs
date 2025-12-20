using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.Services;

namespace EmployeeManagementSystem.Controllers
{
    [Authorize]
    public class PositionController : Controller
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _positionService.GetAllAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var position = await _positionService.GetByIdAsync(id.Value);
            if (position == null) return NotFound();
            return View(position);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PositionId,Name")] Position position)
        {
            if (ModelState.IsValid)
            {
                await _positionService.CreateAsync(position);
                TempData["SuccessMessage"] = "Pozisyon başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            return View(position);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var position = await _positionService.GetByIdAsync(id.Value);
            if (position == null) return NotFound();
            return View(position);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PositionId,Name")] Position position)
        {
            if (id != position.PositionId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _positionService.UpdateAsync(position);
                    TempData["SuccessMessage"] = "Pozisyon başarıyla güncellendi.";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _positionService.ExistsAsync(id)) return NotFound();
                    TempData["ErrorMessage"] = "Pozisyon güncellenirken bir hata oluştu. Lütfen tekrar deneyin.";
                }
            }
            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _positionService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Pozisyon başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
