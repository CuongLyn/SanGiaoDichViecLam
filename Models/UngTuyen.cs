using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models
{
    [Table("tbl_ung_tuyen")]
    public class UngTuyen{
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("id_ung_vien")]
        public int IdUngVien { get; set; }

        [Required]
        [Column("id_cong_viec")]
        public int IdCongViec { get; set; }

        [Column("ngay_ung_tuyen")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime NgayUngTuyen { get; set; } = DateTime.Now;

        [Column("trang_thai")]
        [Required]
        [StringLength(50)]
        public string TrangThai { get; set; } = "Đang chờ";

        [Required]
        [Column("cv")]
        [StringLength(255)]
        public String ?CV {get; set;}

        //Liên kết với NguoiDung
        [ForeignKey("IdUngVien")]
        public NguoiDung ?NguoiDung {get; set;}

        //Lien kết với CongViec
        [ForeignKey("IdCongViec")]
        public CongViec ?CongViec {get; set;}

       


    }

}