using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
    public class KhachHang
    {
        [Key]

        [Required, DisplayName("Mã Khách Hàng")]
        public int ID { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập họ tên sinh viên"), DisplayName("Họ và tên")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Bạn cần chọn giới tính"), DisplayName("Giới Tính")]
        public string GioiTinh { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập số điện thoại"), DisplayName("Số điện thoại")]
        public string SDT { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập địa chỉ"), DisplayName("Địa Chỉ Nhận Hàng")]
        public string DiaChi { get; set; }

        [DataType(DataType.EmailAddress)]
        [DisplayFormat(DataFormatString = "{0:example@domain.com}")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Cần nhập Email"), DisplayName("Email")]
        public string Email { get; set; }

        [Required, DisplayName("Mã Tài Khoản")]
        public int TaiKhoanID { get; set; }

        
    }
}
