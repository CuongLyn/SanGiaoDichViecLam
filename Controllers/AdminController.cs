using Microsoft.AspNetCore.Mvc;
using BTL.Models;
using BTL.Repositories;
using System.Threading.Tasks;
using BTL.PagedResults;

namespace BTL.Controllers
{
    public class AdminController : Controller
    {
        private readonly INguoiDungRepository _nguoiDungRepo;
        private readonly INhaTuyenDungRepository _nhaTuyenDungRepo;
        private readonly IUngVienRepository _ungVienRepo;
        private readonly ICongViecRepository _congViecRepo;

        public AdminController(INguoiDungRepository nguoiDungRepo, 
                                INhaTuyenDungRepository nhaTuyenDungRepo, 
                                IUngVienRepository ungVienRepo, 
                                ICongViecRepository congViecRepo)
        {
            _nguoiDungRepo = nguoiDungRepo;
            _nhaTuyenDungRepo = nhaTuyenDungRepo;
            _ungVienRepo = ungVienRepo;
            _congViecRepo = congViecRepo;
        }

        public async Task<IActionResult> Index()
        {
            var userCount = (await _nguoiDungRepo.GetAllNguoiDungAsync()).Count();
            var employerCount = (await _nhaTuyenDungRepo.GetAllNhaTuyenDungAsync()).Count();
            var jobCount = (await _congViecRepo.GetAllCongViecAsync()).Count();


            ViewBag.UserCount = userCount;
            ViewBag.EmployerCount = employerCount;
            ViewBag.JobCount = jobCount;
            return View("Index");
        }

      
        public async Task<IActionResult> UserManagement(int pageNumber = 1)
        {
            int pageSize = 4; 
            var totalItems = await _nguoiDungRepo.GetAllNguoiDungAsync(); 
            var users = totalItems.Skip((pageNumber - 1) * pageSize).Take(pageSize); 

            var pagedResult = new PagedResult<NguoiDung>
            {
                Items = users,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems.Count()
            };

            return View(pagedResult);
        }

        //Xóa người dùng
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _nguoiDungRepo.DeleteNguoiDungAsync(id);
                if (result) 
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Không tìm thấy người dùng." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }



        //Quản lý nhà tuyển dụng
        public async Task<IActionResult> EmployerManagement(int pageNumber = 1)
        {
            int pageSize = 4;
            var totalItems = await _nhaTuyenDungRepo.GetAllNhaTuyenDungAsync();
            var employers = totalItems.Skip((pageNumber - 1) * pageSize).Take(pageSize); // Phân trang

            var pagedResult = new PagedResult<NhaTuyenDung>
            {
                Items = employers,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems.Count()
            };

            return View(pagedResult);
        }


        //Quản lý công việc
         public async Task<IActionResult> JobManagement(int pageNumber = 1)
        {
            int pageSize = 4;
            var totalItems = await _congViecRepo.GetAllCongViecAsync();
            var jobs = totalItems.Skip((pageNumber - 1) * pageSize).Take(pageSize); // Phân trang

            var pagedResult = new PagedResult<CongViec>
            {
                Items = jobs,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems.Count()
            };

            return View(pagedResult);
        }
        //Xóa công việc
        [HttpDelete]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                await _congViecRepo.DeleteCongViecAsync(id);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


    }

   
}
