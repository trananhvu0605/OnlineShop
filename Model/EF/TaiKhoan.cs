namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TaiKhoan")]
    public partial class TaiKhoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TaiKhoan()
        {
            HoaDons = new HashSet<HoaDon>();
        }

        [Key]
        [StringLength(50)]
        [Required(ErrorMessage = "Tên tài khoản không được để trống")]
        public string TenTK { get; set; }

        [StringLength(50)]
        [Display(Name = "Mật khẩu")]
        public string Mat_Khau { get; set; }

        [StringLength(100)]
        [Display(Name ="Họ và tên")]
        public string HoTen { get; set; }

        [StringLength(255)]
        public string Dia_Chi { get; set; }
        public string SDT { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        public byte? Quyen { get; set; }

        public bool? Khoa { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDon> HoaDons { get; set; }
    }
}
