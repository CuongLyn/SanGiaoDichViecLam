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

        public async Task<IEnumerable<CongViec>> GetDsCongViecByUserIdAsync(int idNguoiDung)
        {
            return await _context.CongViecs
                .Where(cv => cv.IdNhaTuyenDung == idNguoiDung)
                .ToListAsync();
        }

        public async Task AddCongViecAsync(CongViec congViec)
        {
            await _context.CongViecs.AddAsync(congViec);
            await _context.SaveChangesAsync();
        }
    }
}