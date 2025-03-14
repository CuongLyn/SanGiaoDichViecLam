using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models
{
    [Table("tbl_cong_viec")]

    public class CongViec
    {
        //Các cột công việc
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //Id của người dùng
        [Required]
        [Column("id_nha_tuyen_dung")]
        public int IdNhaTuyenDung { get; set; }

        [Column("tieu_de")]
        [Required]
        public String ?TieuDe { get; set; }

        [Column("mo_ta")]
        [Required]
        public String ?MoTa { get; set; }

        [Column("dia_diem")]
        [Required]
        public String ?DiaDiem { get; set; }

        [Column("muc_luong")]
        [Required]
        public String ?MucLuong { get; set; }

        [Column("loai_hinh")]
        [Required]
        public String ?LoaiHinh { get; set; }

        [Column("ngay_dang")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime NgayDang { get; set; } = DateTime.Now;

        //Mối quan hệ với bảng người dùng
        [ForeignKey("IdNhaTuyenDung")]
        public NguoiDung ?NguoiDung { get; set; }

    }
}
