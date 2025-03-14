using BTL.Data;
using BTL.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace BTL.Repositories
{
    public class NguoiDungRepository : GenericRepository<NguoiDung>, INguoiDungRepository
    {
        private readonly AppDbContext _context;

        public NguoiDungRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<NguoiDung?> GetByEmailAsync(string? email)
        {
            return await _context.NguoiDungs.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddNguoiDungAsync(NguoiDung nguoiDung)
        {
            await _context.NguoiDungs.AddAsync(nguoiDung);
            await _context.SaveChangesAsync();
        }
    }
}
