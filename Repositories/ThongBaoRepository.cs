using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BTL.Data;
using BTL.Models;

namespace BTL.Repositories
{
    public class ThongBaoRepository : GenericRepository<ThongBao>, IThongBaoRepository
    {
        private readonly AppDbContext _context;
        public ThongBaoRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        //Thêm thông báo
        public async Task AddThongBaoAsync(ThongBao thongBao)
        {
            await AddAsync(thongBao);
            await SaveChangesAsync();
        }
        //Lấy tất cả thông báo
        public async Task<IEnumerable<ThongBao>> GetAllThongBaoByIdNhaTuyenDung(int idCongTy)
        {
            return await _context.ThongBaos
                .Where(tb => tb.IdCongTy == idCongTy)
                .OrderByDescending(tb => tb.NgayTao)
                .ToListAsync();
        }

        public async Task UpdateThongBaoAsync(ThongBao thongBao)
        {
            Update(thongBao);
            await SaveChangesAsync();
        }

        //Lấy số lượng thông báo chưa đọc
        public async Task<int> GetUnreadNotificationCount(int idCongTy)
        {
            return await _context.ThongBaos
                .Where(tb => tb.IdCongTy == idCongTy && tb.DaXem == false)
                .CountAsync();
        }

        //Lấy thông báo theo id
        public async Task<ThongBao?> GetThongBaoByIdAsync(int id)
        {
            return await _context.ThongBaos
                .FirstOrDefaultAsync(tb => tb.Id == id);
        }

        //Lọc thông báo
        public async Task<List<ThongBao>> GetByNguoiNhanAsync(string nguoiNhan, int idNguoiDung)
        {
            return await _context.ThongBaos
                .Where(tb => tb.NguoiNhan == nguoiNhan &&
                    ((nguoiNhan == "Ứng Viên" && tb.IdUngVien == idNguoiDung) ||
                    (nguoiNhan == "Nhà tuyển dụng" && tb.IdCongTy == idNguoiDung)))
                .OrderByDescending(tb => tb.NgayTao)
                .ToListAsync();
        }

        //Lấy thông báo theo id ung tuyen
        // Lấy thông báo theo IdUngTuyen
        public async Task<ThongBao?> GetByUngTuyenIdAsync(int idUngTuyen)
        {
            return await _context.ThongBaos
                .FirstOrDefaultAsync(tb => tb.IdUngTuyen == idUngTuyen);
        }




    }
}
