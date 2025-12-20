using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.Models;

namespace EmployeeManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly EmployeeManagementContext _context;

        public AccountController(EmployeeManagementContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ViewBag.Error = "Kullanıcı adı veya şifre hatalı.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // Geçici — kullanıcıları ve şifrelerini gösterir, sonra sil
        public IActionResult Debug()
        {
            try
            {
                var users = _context.Users.ToList();
                if (!users.Any())
                    return Content("Users tablosunda hiç kayıt yok.");

                var sb = new System.Text.StringBuilder();
                foreach (var u in users)
                {
                    bool verify1234 = BCrypt.Net.BCrypt.Verify("1234", u.Password ?? "");
                    sb.AppendLine($"Username: {u.Username} | Hash başlangıcı: {u.Password?[..20]}... | 1234 doğru mu: {verify1234}");
                }
                return Content(sb.ToString());
            }
            catch (Exception ex)
            {
                return Content($"HATA: {ex.Message}");
            }
        }

        // Geçici — tüm kullanıcıların şifresini "1234" yapar, sonra sil
        public IActionResult ResetPasswords()
        {
            var users = _context.Users.ToList();
            foreach (var user in users)
                user.Password = BCrypt.Net.BCrypt.HashPassword("1234");
            _context.SaveChanges();
            return Content($"{users.Count} kullanıcının şifresi '1234' olarak sıfırlandı.");
        }
    }
}
