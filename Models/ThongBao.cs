using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models
{
    [Table("tbl_thong_bao")]
    public class ThongBao
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("id_cong_ty")]
        public int IdCongTy { get; set; }
        
        [Required]
        [Column("id_cong_viec")]
       
        public int IdCongViec { get; set; }
        
        [Required]
        [Column("id_ung_vien")]
       
        public int IdUngVien { get; set; }

        [Required]
        [Column("id_ung_tuyen")]
        public int IdUngTuyen { get; set; }
       
        [Column("noi_dung")]
        [Required]
        [StringLength(500)]
        public string ?NoiDung { get; set; }

        [Column("da_xem")]
        public bool DaXem { get; set; } = false;

        [Column("ngay_tao")]
        public DateTime NgayTao { get; set; } = DateTime.Now;

        [Column("loai_thong_bao")]
        [Required]
        [StringLength(255)]
        public string ?LoaiThongBao { get; set; }

        [ForeignKey("IdCongTy")]
        public NhaTuyenDung ?NhaTuyenDung { get; set; }

        [ForeignKey("IdCongViec")]
        public CongViec ?CongViec { get; set; }

        [ForeignKey("IdUngVien")]
        public UngVien ?UngVien { get; set; }

        [ForeignKey("IdUngTuyen")]
        public UngTuyen ?UngTuyen { get; set; }


        //nguoi nhan
        [Column("nguoi_nhan")]
        [Required]
        [StringLength(20)]
        public string? NguoiNhan { get; set; } 

    }
}
