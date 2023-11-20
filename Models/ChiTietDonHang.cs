using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace QuanLyBanGiay.Models
{
    public class ChiTietDonHang
    {
        [Key]
        public int ID { get; set; }
        [Required, DisplayName("Mã Hóa Đơn")]
        [ForeignKey("HoaDon")]
        public int HoaDonID { get; set; }

        [Required, DisplayName("ID Sản Phẩm")]
        [ForeignKey("SanPham")]
        public int SanPhamID { get; set; }

        [Required, DisplayName("Số Lượng Sản Phẩm")]
        public int SoLuongSP { get; set; }

        [Required, DisplayName("Đơn Giá")]
        public double DonGia { get; set; }
    }
}
