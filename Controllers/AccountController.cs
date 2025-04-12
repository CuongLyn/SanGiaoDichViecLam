using BTL.Models;
using BTL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System;

namespace BTL.Controllers
{
    public class AccountController : Controller
    {
        private readonly INguoiDungRepository _nguoiDungRepo;

        public AccountController(INguoiDungRepository nguoiDungRepo)
        {
            _nguoiDungRepo = nguoiDungRepo;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // GET: /Account/Notification
        public IActionResult Notification()
        {
            return View("Notifications");
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(NguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _nguoiDungRepo.GetByEmailAsync(nguoiDung.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email đã tồn tại!");
                    return View(nguoiDung);
                }

                // Mã hóa mật khẩu trước khi lưu
                nguoiDung.MatKhau = HashPassword(nguoiDung.MatKhau);

                await _nguoiDungRepo.AddNguoiDungAsync(nguoiDung);
                return RedirectToAction("Login");
            }
            return View(nguoiDung);
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _nguoiDungRepo.GetByEmailAsync(email);

            if (ModelState.IsValid)
            {
                string hashedPassword = HashPassword(password);

                if (user == null || user.MatKhau != hashedPassword)
                {
                    ModelState.AddModelError("mk", "Email hoặc mật khẩu không đúng!");
                    return View();
                }

                // Lưu thông tin người dùng vào Session
                HttpContext.Session.SetString("Email", user.Email ?? "");
                HttpContext.Session.SetString("HoTen", user.HoTen ?? "");
                HttpContext.Session.SetInt32("Id", user.Id);

                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ Session
            return RedirectToAction("Login");
        }

        // Hàm mã hóa SHA256
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
