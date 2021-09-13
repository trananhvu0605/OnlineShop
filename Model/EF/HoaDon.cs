namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HoaDon")]
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            ChiTiet_HD = new HashSet<ChiTiet_HD>();
        }

        [Key]
        public int MaHD { get; set; }

        [StringLength(100)]
        public string TenKH { get; set; }

        [Required]
        [StringLength(255)]
        public string Dia_Chi_Nhan { get; set; }

        [StringLength(255)]
        public string Yeu_Cau_Khac { get; set; }

        [Required]
        [StringLength(100)]
        public string Tong_Tien { get; set; }
        [Required(ErrorMessage = "Tình trạng không được để trống")]
        public string Tinh_Trang { get; set; }

        public DateTime Ngay_Mua { get; set; }

        [StringLength(50)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(100)]
        public string Sodienthoai { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_HD> ChiTiet_HD { get; set; }

        public virtual TaiKhoan TaiKhoan { get; set; }
    }
}
