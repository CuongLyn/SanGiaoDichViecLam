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

        public ApplicationController( IUngTuyenRepository ungTuyenRepo, 
                                        INguoiDungRepository nguoiDungRepo, 
                                        ICongViecRepository congViecRepo,
                                        IUngVienRepository ungVienRepo)
        {
            _ungTuyenRepo = ungTuyenRepo;
            _nguoiDungRepo = nguoiDungRepo;
            _congViecRepo = congViecRepo;
            _ungVienRepo = ungVienRepo;

        }

        
    }
}