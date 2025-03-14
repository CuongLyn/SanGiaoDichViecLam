using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface ICongViecRepository : IGenericRepository<CongViec>
    {
        Task<IEnumerable<CongViec>> GetDsCongViecByUserIdAsync(int idNguoiDung);
        Task AddCongViecAsync(CongViec congViec);

    }
}