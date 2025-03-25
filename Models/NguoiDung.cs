using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BTL.Models
{
    [Table("tbl_nguoi_dung")]
    public class NguoiDung
    {

        //Thông tin chung cho người dùng
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ho_ten")]
        [Required]
        [StringLength(255)]
        public string ?HoTen { get; set; }

        [Column("email")]
        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string ?Email { get; set; }

        [Column("mat_khau")]
        [Required]
        [StringLength(255)]
        public string ?MatKhau { get; set; }

        [Column("so_dien_thoai")]
        [StringLength(20)]
        public string ?SoDienThoai { get; set; }

        [Column("ngay_tao")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime NgayTao { get; set; } = DateTime.Now;

        //Danh sách công việc
       
        

        //Liên kết với NhaTuyenDung
        public virtual NhaTuyenDung ?NhaTuyenDung { get; set; }

        
    }
}
