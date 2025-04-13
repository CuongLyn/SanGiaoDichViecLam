using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface INguoiDungRepository : IGenericRepository<NguoiDung>
    {
        Task<NguoiDung?> GetByEmailAsync(string? email);
        Task AddNguoiDungAsync(NguoiDung nguoiDung);
        Task<NguoiDung?> GetByIdAsync(int id);
        //Lấy tất cả người dùng
        Task<IEnumerable<NguoiDung>> GetAllNguoiDungAsync();
        //Xóa
        Task<bool> DeleteNguoiDungAsync(int id);
    }
}
