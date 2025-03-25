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

        public async Task AddHoSoUngTuyenAsync( UngTuyen ungTuyen)
        {
            await AddAsync(ungTuyen);
            await SaveChangesAsync();
        }
    }
}