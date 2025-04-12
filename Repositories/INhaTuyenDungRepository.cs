using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface INhaTuyenDungRepository : IGenericRepository<NhaTuyenDung>
    {
        Task<NhaTuyenDung?> GetByUserIdAsync(int userId);
        Task<NhaTuyenDung?> GetByIdAsync(int id);
        Task AddNhaTuyenDungAsync(NhaTuyenDung nhaTuyenDung);
        Task UpdateNhaTuyenDungAsync(NhaTuyenDung nhaTuyenDung);
    }
}