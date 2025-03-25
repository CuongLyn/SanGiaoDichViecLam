using BTL.Models;
using BTL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BTL.Controllers
{
    public class AccountController : Controller
    {
        private readonly INguoiDungRepository _nguoiDungRepo;

        public AccountController(INguoiDungRepository nguoiDungRepo)
        {
            _nguoiDungRepo = nguoiDungRepo;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        //Thông báo
        public IActionResult Notification(){
            return View("Notifications");
        }

        //Đăng ký
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

                await _nguoiDungRepo.AddNguoiDungAsync(nguoiDung);
                return RedirectToAction("Login");
            }
            return View(nguoiDung);
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _nguoiDungRepo.GetByEmailAsync(email);
           
            if(ModelState.IsValid){

                if(user == null || user.MatKhau != password){
                    ModelState.AddModelError("mk", "Email hoặc mật khẩu không đúng!");
                    return View();
                }

                // Lưu thông tin người dùng vào Session
                HttpContext.Session.SetString("Email", user.Email ?? "");
                HttpContext.Session.SetString("HoTen", user.HoTen ?? "");
                HttpContext.Session.SetInt32("Id", user.Id);


                var currentEmail = HttpContext.Session.GetInt32("Id");
                Console.WriteLine(currentEmail);

                return RedirectToAction("Index", "Home");
            }
            
            return View();
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Xóa toàn bộ Session
            return RedirectToAction("Login");
        }

        //Nhà tuyển dụng
       


    

    }
}
