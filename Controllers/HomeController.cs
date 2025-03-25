using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using BTL.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BTL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly INguoiDungRepository _nguoiDungRepo;
        private readonly INhaTuyenDungRepository _nhaTuyenDungRepo;
        private readonly ICongViecRepository _congViecRepo;

        public HomeController(ILogger<HomeController> logger, INguoiDungRepository nguoiDungRepo, INhaTuyenDungRepository nhaTuyenDungRepo, ICongViecRepository congViecRepo)
        {
            _logger = logger;
            _nguoiDungRepo = nguoiDungRepo;
            _nhaTuyenDungRepo = nhaTuyenDungRepo;
            _congViecRepo = congViecRepo;

        }

        public IActionResult Index()
        {
            var danhSachCongViec = _congViecRepo.GetDsCongViecAsync().Result;
            ViewBag.DanhSachCongViec = danhSachCongViec;
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        // Nhà tuyển dụng
        public async Task<IActionResult> RedirectToEmployer()
        {
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account"); // Nếu chưa đăng nhập, chuyển đến trang Login
            }
        

            // Lấy thông tin người dùng từ email
            var nguoiDung = await _nguoiDungRepo.GetByEmailAsync(email);

            if (nguoiDung == null)
            {
                return RedirectToAction("Login", "Account");
            }
            //Kiểm tra người dùng đã có cty hay chưa
            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByUserIdAsync(nguoiDung.Id);

            if(nhaTuyenDung == null)
            {
                return RedirectToAction("EmployerRegister", "Employer"); // Nếu chưa có cty, chuyển đến trang Đăng ký Nhà tuyển dụng
            }

            return RedirectToAction("EmployerProfile", "Employer"); // Nếu đã đăng nhập, chuyển đến trang Nhà tuyển dụng
        }

        // Ứng viên
        public IActionResult RedirectToCandidate()
        {
            var email = HttpContext.Session.GetString("Email");

            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account"); // Nếu chưa đăng nhập, chuyển đến trang Login
            }

            return RedirectToAction("Index", "Home"); // Nếu đã đăng nhập, chuyển đến trang Ứng viên
        }

    }
}
