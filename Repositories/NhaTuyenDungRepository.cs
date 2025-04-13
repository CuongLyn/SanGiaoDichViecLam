using BTL.Data;
using BTL.Models;
using Microsoft.EntityFrameworkCore;
using BTL.Repositories;

namespace BTL.Repositories
{
    public class NhaTuyenDungRepository : GenericRepository<NhaTuyenDung>, INhaTuyenDungRepository
    {
        private readonly AppDbContext _context;

        public NhaTuyenDungRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<NhaTuyenDung?> GetByUserIdAsync(int userId)
        {
            return await _context.NhaTuyenDungs
                .FirstOrDefaultAsync(n => n.NguoiDungId == userId);
        }

        public async Task<NhaTuyenDung?> GetByIdAsync(int id)
        {
            return await _context.NhaTuyenDungs
                .FirstOrDefaultAsync(n => n.Id == id);
        }


        public async Task AddNhaTuyenDungAsync(NhaTuyenDung nhaTuyenDung)
        {
            await AddAsync(nhaTuyenDung);
            await SaveChangesAsync();
        }

        public async Task UpdateNhaTuyenDungAsync(NhaTuyenDung nhaTuyenDung)
        {
            Update(nhaTuyenDung);
            await SaveChangesAsync();
        }

        //Lấy ra tất cả nhà tuyển dụng
        public async Task<IEnumerable<NhaTuyenDung>> GetAllNhaTuyenDungAsync()
        {
            return await GetAllAsync();
        }
    }
}