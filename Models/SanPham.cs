using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace QuanLyBanGiay.Models
{
    public class SanPham
    {
        [Key]
        [Required, DisplayName("Mã Sản Phẩm")]
        public int ID { get; set; }

        [Required, DisplayName("Thương Hiệu")]
        public string ThuongHieu{ get; set; }

        [Required, DisplayName("Màu Sắc")]
        public string MauSac { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tên sản phẩm"), DisplayName("Tên Giày")]
        public string? TenGiay { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập giá sản phẩm"), DisplayName("Giá Bán")]
        public double GiaBan { get; set; }

        [Required(ErrorMessage = "Bạn cần thêm hình ảnh sản phẩm"), DisplayName("Hình Ảnh")]
        [Column("HinhAnhSP")]
        public string? HinhAnh { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập phân loại sản phẩm"), DisplayName("Phân Loại")]
        public string? PhanLoai { get; set; }


    }
}
