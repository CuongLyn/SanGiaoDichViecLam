using BTL.Data;
using BTL.Models;
using BTL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BTL.Repositories
{
    public class UngTuyenRepository : GenericRepository<UngTuyen>, IUngTuyenRepository
    {
        private readonly AppDbContext _context;

        public UngTuyenRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UngTuyen?> GetByUserIdAsync(int userId)
        {
            return await _context.UngTuyens
                .FirstOrDefaultAsync(n => n.IdUngVien == userId);
        }

        public async Task<UngTuyen?> GetByIdAsync(int id)
        {
            return await _context.UngTuyens
                .FirstOrDefaultAsync(n => n.Id == id);
        }
       

        public async Task<UngTuyen?> GetByUserIdAndJobIdAsync(int userId, int jobId)
        {
            return await _context.UngTuyens
                .FirstOrDefaultAsync(ut => ut.IdUngVien == userId && ut.IdCongViec == jobId);
        }


        public async Task AddHoSoUngTuyenAsync( UngTuyen ungTuyen)
        {
            await AddAsync(ungTuyen);
            await SaveChangesAsync();
        }

        //Lấy ra danh sach công việc đã ứng tuyển
        public async Task<List<UngTuyen>> GetJobsAppliedByUserIdAsync(int userId)
        {
            return await _context.UngTuyens
                .Include(u => u.CongViec)
                .Where(u => u.IdUngVien == userId)
                .ToListAsync();
        }

        public async Task UpdateAsync(UngTuyen ungTuyen)
        {
            _context.UngTuyens.Update(ungTuyen);
            await _context.SaveChangesAsync();
        }


    }
}