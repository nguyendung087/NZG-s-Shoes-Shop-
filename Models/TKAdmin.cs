using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyBanGiay.Models
{
    [Area("Admin")]
    public class TKAdmin
    {
        [Key]

        [Required, DisplayName("Mã Admin")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập tài khoản"), DisplayName("Tài Khoản Admin")]
        public string TaiKhoanAdmin { get; set; }

        [Required(ErrorMessage = "Bạn cần nhập mật khẩu"), DisplayName("Mật Khẩu Admin")]
        public string MatKhauAdmin { get; set; }
    }
}
