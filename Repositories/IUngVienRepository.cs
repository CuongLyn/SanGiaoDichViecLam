using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface IUngVienRepository : IGenericRepository<UngVien>
    {
        Task<UngVien?> GetByUserIdAsync(int userId);
        Task AddHoSoUngVienAsync(UngVien ungVien);
    }
}