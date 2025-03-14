using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using BTL.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BTL.Controllers
{
    public class EmployerController : Controller
    {
        private readonly INhaTuyenDungRepository _nhaTuyenDungRepo;
        private readonly INguoiDungRepository _nguoiDungRepo;
        private readonly ICongViecRepository _congViecRepo;
       

        public EmployerController(INhaTuyenDungRepository nhaTuyenDungRepo, INguoiDungRepository nguoiDungRepo, ICongViecRepository congViecRepo)
        {
            _nhaTuyenDungRepo = nhaTuyenDungRepo;
            _nguoiDungRepo = nguoiDungRepo;
            _congViecRepo = congViecRepo;
    
        }

        public IActionResult EmployerRegister()
        {
            return View("EmployerRegister");
        }

        public async Task<IActionResult> EmployerProfile()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy thông tin người dùng từ email
            var nguoiDung = await _nguoiDungRepo.GetByEmailAsync(email);
            if (nguoiDung == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy thông tin nhà tuyển dụng từ UserId
            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByUserIdAsync(nguoiDung.Id);
            if (nhaTuyenDung == null)
            {
                return RedirectToAction("EmployerRegister", "Employer");
            }
           

            //Lấy danh sách công việc của nhà tuyển dụng
            var danhSachCongViec = await _congViecRepo.GetDsCongViecByUserIdAsync(nhaTuyenDung.NguoiDungId);
            ViewBag.nhaTuyenDung = nhaTuyenDung;
            ViewBag.DanhSachCongViec = danhSachCongViec;

           
            return View("EmployerProfile", nhaTuyenDung);
        }

        // Đăng ký Nhà Tuyển Dụng
        [HttpPost]
        public async Task<IActionResult> EmployerRegister(NhaTuyenDung nhaTuyenDung)
        {
            int? nguoiDungId = HttpContext.Session.GetInt32("Id");
            if (nguoiDungId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            nhaTuyenDung.NguoiDungId = nguoiDungId.Value;
            await _nhaTuyenDungRepo.AddNhaTuyenDungAsync(nhaTuyenDung);

            return RedirectToAction("EmployerProfile", "Employer"); // Chuyển hướng sau khi đăng ký
        }




    }
}
