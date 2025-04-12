using Microsoft.AspNetCore.Mvc;
using BTL.Repositories;
using BTL.Models;
using AspNetCoreGeneratedDocument;

namespace BTL.Controllers
{
    public class NotificationController : Controller
    {
        private readonly IThongBaoRepository _thongBaoRepo;
        private readonly INguoiDungRepository _nguoiDungRepo;
        private readonly INhaTuyenDungRepository _nhaTuyenDungRepo;
        private readonly IUngTuyenRepository _ungTuyenRepo;
        private readonly IUngVienRepository _ungVienRepo;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly ICongViecRepository _congViecRepository;


        public NotificationController(IThongBaoRepository thongBaoRepo,
                                        INguoiDungRepository nguoiDungRepo,
                                        INhaTuyenDungRepository nhaTuyenDungrepo,
                                        IUngTuyenRepository ungTuyenRepo,
                                        IUngVienRepository ungVienRepo,
                                        INguoiDungRepository nguoiDungRepository,
                                        ICongViecRepository congViecRepository)
        {
            _thongBaoRepo = thongBaoRepo;
            _nguoiDungRepo = nguoiDungRepo;
            _nhaTuyenDungRepo = nhaTuyenDungrepo; 
            _ungTuyenRepo = ungTuyenRepo;
            _ungVienRepo = ungVienRepo;
            _nguoiDungRepository = nguoiDungRepository;
            _congViecRepository = congViecRepository;
        }

        

         // Lấy số lượng thông báo chưa đọc
        public async Task<IActionResult> GetNotificationCount()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { count = 0 });
            }

            var nguoiDung = await _nguoiDungRepo.GetByEmailAsync(email);
            if (nguoiDung == null)
            {
                return Json(new { count = 0 });
            }

            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByUserIdAsync(nguoiDung.Id);
            if (nhaTuyenDung == null)
            {
                return Json(new { count = 0 });
            }

            var thongBaos = await _thongBaoRepo.GetUnreadNotificationCount(nhaTuyenDung.Id);
            return Json(new { count = thongBaos });
        }

        // Lấy danh sách thông báo
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(email))
            {
                return Json(new { success = false, message = "Người dùng chưa đăng nhập" });
            }

            var nguoiDung = await _nguoiDungRepo.GetByEmailAsync(email);
            if (nguoiDung == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thông tin người dùng" });
            }

            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByUserIdAsync(nguoiDung.Id);
            if (nhaTuyenDung == null)
            {
                return Json(new { success = false, message = "Không tìm thấy thông tin công ty" });
            }

            var notifications = await _thongBaoRepo.GetAllThongBaoByIdNhaTuyenDung(nhaTuyenDung.Id);
            var result = notifications.Select(tb => new
            {
                id = tb.Id,
                title = tb.NoiDung,
                daXem = tb.DaXem,
                ngayTao = tb.NgayTao.ToString("dd/MM/yyyy HH:mm")
            });

            return Json(result);
        }
        


        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var thongBao = await _thongBaoRepo.GetThongBaoByIdAsync(id);
            var nhaTuyenDung = await _nhaTuyenDungRepo.GetByIdAsync(thongBao.IdCongTy);
            if (thongBao == null)
            {
                return Json(new { success = false, message = "Thông báo không tồn tại" });
            }

            

            // Nếu chưa đọc, mới đánh dấu và gửi thông báo cho ứng viên
            thongBao.DaXem = true;
            await _thongBaoRepo.UpdateThongBaoAsync(thongBao);

            // var thongBaoChoUngVien = new ThongBao
            // {
            //     IdCongTy = thongBao.IdCongTy,
            //     IdCongViec = thongBao.IdCongViec,
            //     IdUngVien = thongBao.IdUngVien,
            //     NoiDung = $"Nhà tuyển dụng {nhaTuyenDung.TenCongTy} đã xem CV của bạn",
            //     DaXem = false,
            //     NgayTao = DateTime.Now,
            //     LoaiThongBao = "Nhà tuyển dụng đã xem CV",
            //     IdUngTuyen = thongBao.IdUngTuyen,
            //     NguoiNhan = "Ứng viên"

            // };

            

            // await _thongBaoRepo.AddThongBaoAsync(thongBaoChoUngVien);

            return Json(new { success = true, message = "Thông báo đã được đánh dấu là đã đọc" });
        }



        public async Task<IActionResult> Detail(int id)
        {
            var thongBao = await _thongBaoRepo.GetThongBaoByIdAsync(id);
          

            var ungTuyen = await _ungTuyenRepo.GetByIdAsync(thongBao.IdUngTuyen);
            

            var ungVien = await _ungVienRepo.GetByUserIdAsync(ungTuyen.IdUngVien);
            
            var nguoiDung = await _nguoiDungRepository.GetByIdAsync(ungVien.NguoiDungId);
            

            var congViec = await _congViecRepository.GetCongViecByIdAsync(thongBao.IdCongViec);
           

            //Thông tin của ứng viên
            ViewBag.GioiThieu = ungVien.GioiThieu;
            ViewBag.TenUngVien = nguoiDung.HoTen;
            ViewBag.SDTUngVien = nguoiDung.SoDienThoai;
            ViewBag.EmailUngVien = nguoiDung.Email;

            //Id Ứng tuyển
            ViewBag.UngTuyenId = ungTuyen.Id;
            ViewBag.TrangThai = ungTuyen.TrangThai;

            ViewBag.CV = ungTuyen.CV;

            //Thông tin về công việc
            ViewBag.ViTriUngTuyen = congViec.TieuDe;

            return View("Detail_Ung_Tuyen", thongBao);
        }

        //Cập nhật trạng thái
        [HttpPost]
        public async Task<IActionResult> UpdateTrangThai(int ungTuyenId, string trangThai)
        {
            var ungTuyen = await _ungTuyenRepo.GetByIdAsync(ungTuyenId);

            var thongBao = await _thongBaoRepo.GetByUngTuyenIdAsync(ungTuyenId);

            Console.WriteLine("ID ứng tuyển: " + ungTuyenId);
            if (ungTuyen == null)
                return NotFound("Không tìm thấy thông tin ứng tuyển");

            // Cập nhật trạng thái
            ungTuyen.TrangThai = trangThai; // "Pass" hoặc "Fail"
            await _ungTuyenRepo.UpdateAsync(ungTuyen); // Đảm bảo repository có hàm UpdateAsync

            // Có thể gửi thêm thông báo cho ứng viên nếu muốn

            TempData["Message"] = "Cập nhật trạng thái thành công!";
            return RedirectToAction("Detail", new { id = thongBao.Id }); 
        }


       



    }
}