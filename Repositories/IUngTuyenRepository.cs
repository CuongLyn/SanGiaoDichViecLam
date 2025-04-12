using BTL.Models;
using System.Threading.Tasks;

namespace BTL.Repositories
{
    public interface IUngTuyenRepository : IGenericRepository<UngTuyen>
    {
        Task<UngTuyen?> GetByUserIdAsync(int userId);
        Task<UngTuyen?> GetByIdAsync(int id);
        Task<UngTuyen?> GetByUserIdAndJobIdAsync(int userId, int jobId);
        Task AddHoSoUngTuyenAsync( UngTuyen ungTuyen);

        //Lấy ra danh sach công việc đã ứng tuyển
        Task<List<UngTuyen>> GetJobsAppliedByUserIdAsync(int userId);

        //Cập nhật trạng thái
        Task UpdateAsync(UngTuyen ungTuyen);

        
    }
}