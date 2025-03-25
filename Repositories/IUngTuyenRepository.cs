using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface IUngTuyenRepository : IGenericRepository<UngTuyen>
    {
        Task<UngTuyen?> GetByUserIdAsync(int userId);
        Task AddHoSoUngTuyenAsync( UngTuyen ungTuyen);
    }
}