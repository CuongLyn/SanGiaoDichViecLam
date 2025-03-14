using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using BTL.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BTL.Controllers
{
    public class JobController : Controller{

        private readonly ICongViecRepository _congViecRepo;
        private readonly INhaTuyenDungRepository _nhaTuyenDungRepo;
        private readonly INguoiDungRepository _nguoiDungRepo;

        public JobController(ICongViecRepository congViecRepo, INhaTuyenDungRepository nhaTuyenDungRepo, INguoiDungRepository nguoiDungRepo)
        {
            _congViecRepo = congViecRepo;
            _nhaTuyenDungRepo = nhaTuyenDungRepo;
            _nguoiDungRepo = nguoiDungRepo;
        }

        public IActionResult CreateJob()
        {
            return View("Create");
        }
        [HttpPost]
        public async Task<IActionResult> CreateJob(CongViec congViec)
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

            congViec.IdNhaTuyenDung = nhaTuyenDung.Id;
            await _congViecRepo.AddCongViecAsync(congViec);

            return RedirectToAction("EmployerProfile", "Employer");
        }
    }
}