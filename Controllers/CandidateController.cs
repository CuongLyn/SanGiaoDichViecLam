using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BTL.Data;
using BTL.Models;
using BTL.Repositories;

namespace BTL.Controllers
{
    public class CandidateController : Controller
    {
        private readonly IUngVienRepository _ungVienRepo;
        private readonly INhaTuyenDungRepository _nhaTuyenDungRepo;
        private readonly INguoiDungRepository _nguoiDungRepo;




        public CandidateController(IUngVienRepository ungVienRepo, INhaTuyenDungRepository nhaTuyenDungRepo, INguoiDungRepository nguoiDungRepo)
        {
            _ungVienRepo = ungVienRepo;
            _nhaTuyenDungRepo = nhaTuyenDungRepo;
            _nguoiDungRepo = nguoiDungRepo;
        }

        public IActionResult Create(){
            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(UngVien ungVien, IFormFile? cvFile)
        {
            var userId = HttpContext.Session.GetInt32("Id");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra xem người này đã có hồ sơ chưa
            var existingUngVien = await _ungVienRepo.GetByUserIdAsync(userId.Value);
            if (existingUngVien != null)
            {
                return RedirectToAction("CandidateProfile", new { id = existingUngVien.Id });
            }

            if (cvFile != null && cvFile.Length > 0)
            {
                // Đảm bảo thư mục uploads tồn tại
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Lưu file với tên duy nhất
                var fileName = $"{Guid.NewGuid()}_{cvFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await cvFile.CopyToAsync(stream);
                }

                // Lưu tên file vào database
                ungVien.CV = fileName;
            }

            ungVien.NguoiDungId = userId.Value;
            await _ungVienRepo.AddHoSoUngVienAsync(ungVien);

            return RedirectToAction("CandidateProfile", new { id = ungVien.Id });
        }


        public async Task<IActionResult> CandidateProfile()
        {
            var userId = HttpContext.Session.GetInt32("Id");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Kiểm tra xem hồ sơ ứng viên đã tồn tại chưa
            var existingUngVien = await _ungVienRepo.GetByUserIdAsync(userId.Value);
            
            if (existingUngVien == null)
            {
                return RedirectToAction("Create");
            }
            return View("CandidateProfile", existingUngVien);
        }


    }
}
