using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BTL.Models;

namespace BTL.Models
{
    [Table("tbl_ho_so_ung_vien")]
    public class UngVien
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required]
        [Column("nguoi_dung_id")]
        public int NguoiDungId {get; set;}

        [Required]
        [Column("gioi_thieu")]
        [StringLength(255)]
        public String ?GioiThieu {get; set;}

        [Required]
        [Column("ky_nang")]
        [StringLength(255)]
        public String ?KyNang {get; set;}

        [Required]
        [Column("kinh_nghiem")]
        [StringLength(255)]
        public String ?KinhNghiem {get; set;}

        [Required]
        [Column("cv")]
        [StringLength(255)]
        public String ?CV {get; set;}

        [ForeignKey("NguoiDungId")]
        public NguoiDung ?NguoiDung {get; set;}

        //Ds công việc
      
        

        
        
    }
}