using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Infrastructure;
using QuanLyBanGiay.Models;

namespace QuanLyBanGiay.Controllers
{
    public class CheckoutController : BaseController
    {
        public readonly QLBanGiayDBContext _context;
        public CheckoutController(QLBanGiayDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DonHang()
        {
            var user = _context.TaiKhoanKHs.FirstOrDefault(m => m.TaiKhoan == CurrentUser);
            if (user == null)
            {
                return NotFound();
            }
            var thongtin = _context.KhachHangs.Where(m => m.TaiKhoanID == user.ID).ToList();
            if (thongtin != null)
            {
                return View(thongtin);
            }
            return View();
        }
    }
}
