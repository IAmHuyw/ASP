namespace traicay.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SanPham")]
    public partial class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Mã sản phẩm")]
        [Required]
        public int MaSP { get; set; }

        [StringLength(100)]
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = ("Vui lòng nhập tên sản phẩm"))]
        public string TenSP { get; set; }
        [DisplayName("Số lượng")]
        [Required(ErrorMessage = ("Vui lòng nhập số lượng"))]
        [Range(1,int.MaxValue, ErrorMessage = "Số lượng là số nguyên và lớn hơn 0")]
        public int? SoLuong { get; set; }
        [DisplayName("Đơn giá")]
        [Required(ErrorMessage = ("Vui lòng nhập đơn giá"))]
        [Range(1, int.MaxValue, ErrorMessage = "Đơn giá là số nguyên và lớn hơn 0")]
        public decimal? DonGia { get; set; }

        [StringLength(50)]
        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = ("Vui lòng tải ảnh lên"))]
        public string HinhAnh { get; set; }
        [DisplayName("Hãng sản xuất")]
        [Required(ErrorMessage = ("Vui lòng nhập hãng sản xuất"))]
        public int? MaHang { get; set; }

        public virtual HangSanXuat HangSanXuat { get; set; }
    }
}
