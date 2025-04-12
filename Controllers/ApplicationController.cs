using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using BTL.Repositories;

namespace BTL.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IUngTuyenRepository _ungTuyenRepo;
        private readonly INguoiDungRepository _nguoiDungRepo;
        private readonly ICongViecRepository _congViecRepo;
        private readonly IUngVienRepository _ungVienRepo;
        private readonly IThongBaoRepository _thongBaoRepo;

        public ApplicationController( IUngTuyenRepository ungTuyenRepo, 
                                        INguoiDungRepository nguoiDungRepo, 
                                        ICongViecRepository congViecRepo,
                                        IUngVienRepository ungVienRepo,
                                        IThongBaoRepository thongBaoRepo)
        {
            _ungTuyenRepo = ungTuyenRepo;
            _nguoiDungRepo = nguoiDungRepo;
            _congViecRepo = congViecRepo;
            _ungVienRepo = ungVienRepo;
            _thongBaoRepo = thongBaoRepo;

        }

        [HttpPost]
        [Route("Application/ApplyJob")]
        public async Task<IActionResult> ApplyJob(int jobId, IFormFile cv, string fullName, string email, string phone)
        {
            if (cv == null || cv.Length == 0)
            {
                return BadRequest("Vui lòng tải lên CV.");
            }

            // Tạo thư mục nếu chưa tồn tại
            var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Lưu file CV vào thư mục
            var fileName = $"{Guid.NewGuid()}_{cv.FileName}"; // Đảm bảo tên file không trùng
            var filePath = Path.Combine(uploadFolder, fileName);
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await cv.CopyToAsync(stream);
            }

            // Kiểm tra nếu đã ứng tuyển rồi
            var user = await _nguoiDungRepo.GetByEmailAsync(email);
            if (user == null)
            {
                return BadRequest("Người dùng không tồn tại.");
            }

            var existingApplication = await _ungTuyenRepo.GetByUserIdAndJobIdAsync(user.Id, jobId);
            if (existingApplication != null)
            {
                return BadRequest("Bạn đã ứng tuyển công việc này.");
            }

            // Tạo mới ứng tuyển
            var ungTuyen = new UngTuyen
            {
                IdUngVien = user.Id,
                IdCongViec = jobId,
                NgayUngTuyen = DateTime.Now,
                TrangThai = "Đang chờ",
                CV = $"/uploads/{fileName}" // Lưu đường dẫn tương đối
            };

            var job = await _congViecRepo.GetCongViecByIdAsync(jobId);
            if (job == null)
            {
                return BadRequest("Công việc không tồn tại");
            }

            await _ungTuyenRepo.AddHoSoUngTuyenAsync(ungTuyen);

            var thongBao = new ThongBao
            {
                IdCongTy = job.IdNhaTuyenDung,
                IdCongViec = jobId,
                IdUngVien = user.Id,
                NoiDung = $"Ứng viên {user.HoTen} đã ứng tuyển vào công việc {job.TieuDe}.",
                DaXem = false,
                NgayTao = DateTime.Now,
                LoaiThongBao = "Ứng viên nộp đơn ứng tuyển",
                IdUngTuyen = ungTuyen.Id,
                NguoiNhan = "Nhà tuyển dụng"
            };

            await _thongBaoRepo.AddThongBaoAsync(thongBao);
            

            return Ok("Ứng tuyển và gửi thông báo thành công!");
        }




        
    }
}