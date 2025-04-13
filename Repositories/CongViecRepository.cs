using BTL.Data;
using BTL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BTL.Repositories
{
    public class CongViecRepository : GenericRepository<CongViec>, ICongViecRepository
    {
        private readonly AppDbContext _context;

        public CongViecRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        //Lấy danh sách công việc
        public async Task<IEnumerable<CongViec>> GetDsCongViecAsync()
        {
            return await _context.CongViecs
                .Include(cv => cv.NhaTuyenDung)
                .ToListAsync();
        }

        //Lấy danh sách công việc theo id người dùng
        public async Task<IEnumerable<CongViec>> GetDsCongViecByUserIdAsync(int IdNhaTuyenDung)
        {
            return await _context.CongViecs
                .Where(cv => cv.NhaTuyenDung != null && cv.NhaTuyenDung.NguoiDungId == IdNhaTuyenDung)
                .ToListAsync();
        }
        //Thêm công việc
        public async Task AddCongViecAsync(CongViec congViec)
        {
            await AddAsync(congViec);
            await SaveChangesAsync();
        }
        //Lấy công việc theo id
        public async Task<CongViec> GetCongViecByIdAsync(int id)
        {
            var congViec = await _context.CongViecs
                .Include(cv => cv.NhaTuyenDung)
                .FirstOrDefaultAsync(cv => cv.Id == id);

            return congViec ?? new CongViec();
        }

        //Chỉnh sửa công việc
        public async Task UpdateCongViecAsync(CongViec congViec)
        {
            Update(congViec);
            await SaveChangesAsync();
        }
        //Xóa công việc
        public async Task DeleteCongViecAsync(int id)
        {
            var congViec = await GetCongViecByIdAsync(id);
            if (congViec != null)
            {
                Delete(congViec);
                await SaveChangesAsync();
            }
        }
        //Tìm kiếm
        public async Task<List<CongViec>> TimKiemCongViecAsync(string tuKhoa, string diaDiem)
        {
            var query = _context.CongViecs
                .Include(cv => cv.NhaTuyenDung)
                .AsQueryable();

            if (!string.IsNullOrEmpty(tuKhoa))
            {
                query = query.Where(cv => cv.TieuDe.Contains(tuKhoa) || cv.MoTa.Contains(tuKhoa));
            }

        

            if (!string.IsNullOrEmpty(diaDiem))
            {
                query = query.Where(cv => cv.DiaDiem.Contains(diaDiem));
            }

            return await query.ToListAsync();
        }

        //Get all
        public async Task<List<CongViec>> GetAllCongViecAsync()
        {
            return await _context.CongViecs
                .Include(cv => cv.NhaTuyenDung)
                .ToListAsync();
        }


       
    }
}