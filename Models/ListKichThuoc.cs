using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanGiay.Models
{
    public class ListKichThuoc
    {
        [Key]
        [Required, DisplayName("Mã Kích Thước")]
        public int? ID { get; set; }

        [Required, DisplayName("Loại Kích Thước")]
        public string? LoaiKichThuoc { get; set; }
    }
}
