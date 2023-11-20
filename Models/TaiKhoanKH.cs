using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanGiay.Models
{
	public class TaiKhoanKH
	{
		[Key]
		public int ID { get; set; }

		[Required(ErrorMessage = "Bạn cần nhập tài khoản"), DisplayName("Tài Khoản")]
		public string TaiKhoan { get; set; }

		[Required(ErrorMessage = "Bạn cần nhập mật khẩu"), DisplayName("Mật Khẩu")]
		public string MatKhau { get; set; }
	}
}
