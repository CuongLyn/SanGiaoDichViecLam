using BTL.Data;
using BTL.Models;
using BTL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BTL.Repositories
{
    public class UngVienRepository : GenericRepository<UngVien>, IUngVienRepository
    {
        private readonly AppDbContext _context;

        public UngVienRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UngVien?> GetByUserIdAsync(int userId)
        {
            return await _context.UngViens
                .FirstOrDefaultAsync(n => n.NguoiDungId == userId);
        }

        public async Task AddHoSoUngVienAsync(UngVien ungVien)
        {
            await AddAsync(ungVien);
            await SaveChangesAsync();
        }



    }
}