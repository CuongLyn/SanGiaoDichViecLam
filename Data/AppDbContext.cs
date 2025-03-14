using Microsoft.EntityFrameworkCore;
using BTL.Models;

namespace BTL.Data{

    public class AppDbContext : DbContext{
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<NhaTuyenDung> NhaTuyenDungs { get; set; }
        public DbSet<CongViec> CongViecs { get; set; }
        
      
        
    }
}
