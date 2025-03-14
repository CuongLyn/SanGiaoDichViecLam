using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface INguoiDungRepository : IGenericRepository<NguoiDung>
    {
        Task<NguoiDung?> GetByEmailAsync(string? email);
        Task AddNguoiDungAsync(NguoiDung nguoiDung);
    }
}
