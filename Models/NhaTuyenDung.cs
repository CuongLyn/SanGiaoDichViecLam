using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTL.Models
{
    [Table("tbl_cong_ty")]
    public class NhaTuyenDung
    {

        //Thông tin chung cho người dùng
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId { get; set; }

        [Column("ten_cong_ty")]
        [Required]
        [StringLength(255)]
        public string ?TenCongTy { get; set; }

        [Column("dia_chi")]
        [Required]
        [StringLength(255)]
        public string ?DiaChi { get; set; }

        [Column("mo_ta_cong_ty")]
        [Required]
        [StringLength(255)]
        public string ?MoTaCongTy { get; set; }

        [Column("website")]
        [StringLength(20)]
        public string ?Website { get; set; }

        [Column("logo")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ?Logo { get; set; }

       // Mối quan hệ với người dùng (Chủ sở hữu)
        [ForeignKey("NguoiDungId")]
        public NguoiDung ?NguoiDung { get; set; }

        

   
    }
}