using System.Collections.Generic;
using System.Threading.Tasks;
using BTL.Models;

namespace BTL.Repositories
{
    public interface IThongBaoRepository : IGenericRepository<ThongBao>
    {
        Task<IEnumerable<ThongBao>> GetAllThongBaoByIdNhaTuyenDung(int IdNhaTuyenDung); //Lấy tất cả thông báo
        Task AddThongBaoAsync(ThongBao thongBao);
        Task UpdateThongBaoAsync(ThongBao thongBao);
        
        //Lọc thông báo
        Task<List<ThongBao>> GetByNguoiNhanAsync(string nguoiNhan, int idNguoiDung);

        Task<int> GetUnreadNotificationCount(int idCongTy);
        Task<ThongBao> GetThongBaoByIdAsync(int id);

        Task<ThongBao?> GetByUngTuyenIdAsync(int idUngTuyen); 
    }
}
