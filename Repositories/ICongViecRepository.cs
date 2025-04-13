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

        //Update công việc
        Task UpdateCongViecAsync(CongViec congViec);

        //Xóa công việc
        Task DeleteCongViecAsync(int id);

        //Tìm kiếm
        Task<List<CongViec>> TimKiemCongViecAsync(string tuKhoa, string diaDiem);

        //Get all
        Task<List<CongViec>> GetAllCongViecAsync();

    }
}