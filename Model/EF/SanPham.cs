namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SanPham()
        {
            ChiTiet_HD = new HashSet<ChiTiet_HD>();
        }

        [Key]
        [StringLength(20)]
        [Required(ErrorMessage = "Mã sản phẩm không được để trống")]
        public string MaSP { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(100)]
        public string TenSP { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Ảnh không được để trống")]
        public string Anh { get; set; }

        [Required(ErrorMessage = "Xuất xứ không được để trống")]
        [StringLength(20)]
        public string Xuat_Xu { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Điểm bán không được để trống")]
        public string Diem_Ban { get; set; }

        public bool DVT { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống")]
        public int SoLuongCo { get; set; }
        [DisplayFormat(DataFormatString = "{0:#,###}")]
        [Required(ErrorMessage = "Giá không được để trống")]
        public double? Gia_HT { get; set; }
        [Required(ErrorMessage = "Mô tả không được để trống")]
        public string Mo_Ta { get; set; }

        [Required(ErrorMessage = "Mã loại sản phẩm không được để trống")]
        [StringLength(20)]
        public string MaDM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTiet_HD> ChiTiet_HD { get; set; }

        public virtual DanhMuc DanhMuc { get; set; }
    }
}
