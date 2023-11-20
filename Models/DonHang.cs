using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
    public class DonHang
    {
        [Key]

        [Required, DisplayName("Mã Hóa Đơn")]
        public int ID { get; set; }

        [Required, DisplayName("ID Khách Hàng")]
        [ForeignKey("KhachHang")]
        public int KhachHangID { get; set; }

        //[Required, DisplayName("ID Sản Phẩm")]
        //[ForeignKey("SanPham")]
        //public int SanPhamID { get; set; }

        [DataType(DataType.Date)]
        [Required, DisplayName("Ngày Đặt Hàng")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime NgayDatHang { get; set; }

        public virtual ChiTietDonHang? chiTietDonHang { get; set; }
    }
}
