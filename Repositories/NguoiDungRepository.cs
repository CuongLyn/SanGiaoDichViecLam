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

        public async Task<NguoiDung?> GetByIdAsync(int id)
        {
            return await _context.NguoiDungs.FindAsync(id);
        }

        //Lấy tất cả người dùng
        public async Task<IEnumerable<NguoiDung>> GetAllNguoiDungAsync()
        {
            return await GetAllAsync();
        }

        //Xóa người dùng
        public async Task<bool> DeleteNguoiDungAsync(int id)
        {
            var nguoiDung = await GetByIdAsync(id);
            if (nguoiDung != null)
            {
                Delete(nguoiDung); // Sử dụng phương thức Remove kế thừa từ GenericRepository
                await SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
                return true; // Trả về true nếu xóa thành công
            }
            return false; // Trả về false nếu không tìm thấy người dùng
        }


    }
}
