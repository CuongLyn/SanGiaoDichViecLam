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

        public JobController(ICongViecRepository congViecRepo, INhaTuyenDungRepository nhaTuyenDungRepo, INguoiDungRepository nguoiDungRepo )
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

            var nguoiDung = await _nguoiDungRepo.GetByEmailAsync(email);
            if (nguoiDung == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByUserIdAsync(nguoiDung.Id);
            if (nhaTuyenDung == null)
            {
                return RedirectToAction("EmployerRegister", "Employer");
            }

            congViec.IdNhaTuyenDung = nhaTuyenDung.Id;
            await _congViecRepo.AddCongViecAsync(congViec);

            return RedirectToAction("EmployerProfile", "Employer");
        }
        //Chi tiết
        public async Task<IActionResult> Detail(int id)
        {
            var congViec = await _congViecRepo.GetCongViecByIdAsync(id);
            if (congViec == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Detail", congViec);
        }

        //Danh sách công việc đã ứng tuyển
         //Sửa công việc
        public async Task<IActionResult> Edit(int id)
        {
            var congViec = await _congViecRepo.GetCongViecByIdAsync(id);
            if (congViec == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Edit", congViec);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CongViec congViec)
        {
            
            var existingCongViec = await _congViecRepo.GetCongViecByIdAsync(congViec.Id);
            
            existingCongViec.TieuDe = congViec.TieuDe;
            existingCongViec.MoTa = congViec.MoTa;
            existingCongViec.MucLuong = congViec.MucLuong;
            existingCongViec.LoaiHinh = congViec.LoaiHinh;
            existingCongViec.NgayDang = congViec.NgayDang;

            await _congViecRepo.UpdateCongViecAsync(existingCongViec);

            return RedirectToAction("EmployerProfile", "Employer");
        }

        //Xóa công việc
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _congViecRepo.DeleteCongViecAsync(id);
            return RedirectToAction("EmployerProfile", "Employer");
        }



       

       

    }
}