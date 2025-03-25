using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface ICongViecRepository : IGenericRepository<CongViec>
    {
        Task<IEnumerable<CongViec>> GetDsCongViecAsync();
        Task<IEnumerable<CongViec>> GetDsCongViecByUserIdAsync(int IdNhaTuyenDung);
        Task AddCongViecAsync(CongViec congViec);
        Task<CongViec> GetCongViecByIdAsync(int id);

    }
}