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
        //Đăng ký
        [HttpPost]
        public async Task<IActionResult> EmployerRegister(NhaTuyenDung nhaTuyenDung, IFormFile? LogoFile)
        {
            int? nguoiDungId = HttpContext.Session.GetInt32("Id");
            if (nguoiDungId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            nhaTuyenDung.NguoiDungId = nguoiDungId.Value;
            var tenCT = nhaTuyenDung.TenCongTy;
        

            // Kiểm tra file logo trước khi lưu
            if (LogoFile != null && LogoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{tenCT}_{Path.GetFileName(LogoFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }

                // Kiểm tra xem file đã tồn tại chưa
                if (System.IO.File.Exists(filePath))
                {
                    Console.WriteLine($"✅ Logo đã được tải lên: {filePath}");
                    nhaTuyenDung.Logo = $"/uploads/{uniqueFileName}";
                }
                else
                {
                    Console.WriteLine("❌ Lỗi: File không tồn tại sau khi tải lên!");
                    ModelState.AddModelError("LogoFile", "Không thể tải lên logo. Vui lòng thử lại.");
                    return View("EmployerRegister", nhaTuyenDung);
                }
            }
            else
            {
                Console.WriteLine("⚠️ Không có logo được tải lên.");
            }

            await _nhaTuyenDungRepo.AddNhaTuyenDungAsync(nhaTuyenDung);
            return RedirectToAction("EmployerProfile", "Employer");
        }

        //Edit
        public async Task<IActionResult> Edit(int id)
        {
            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByIdAsync(id);
            if (nhaTuyenDung == null)
            {
                return NotFound();
            }
            return View("Edit", nhaTuyenDung);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NhaTuyenDung nhaTuyenDung, IFormFile? LogoFile)
        {
            if (!ModelState.IsValid)
            {
                return View("Edit", nhaTuyenDung);
            }

            var existingNhaTuyenDung = await _nhaTuyenDungRepo.GetByIdAsync(nhaTuyenDung.Id);
            if (existingNhaTuyenDung == null)
            {
                return NotFound();
            }

            if (LogoFile != null && LogoFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = $"{nhaTuyenDung.TenCongTy}_{Path.GetFileName(LogoFile.FileName)}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await LogoFile.CopyToAsync(stream);
                }

                if (System.IO.File.Exists(filePath))
                {
                    existingNhaTuyenDung.Logo = $"/uploads/{uniqueFileName}";
                }
                else
                {
                    ModelState.AddModelError("LogoFile", "Không thể tải lên logo. Vui lòng thử lại.");
                    return View("Edit", nhaTuyenDung);
                }
            }

            existingNhaTuyenDung.TenCongTy = nhaTuyenDung.TenCongTy;
            existingNhaTuyenDung.DiaChi = nhaTuyenDung.DiaChi;
            existingNhaTuyenDung.MoTaCongTy = nhaTuyenDung.MoTaCongTy;
            existingNhaTuyenDung.Website = nhaTuyenDung.Website;

            await _nhaTuyenDungRepo.UpdateNhaTuyenDungAsync(existingNhaTuyenDung);

            return RedirectToAction("EmployerProfile", "Employer");
        }

        

        



    }
}
