using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Infrastructure;
using QuanLyBanGiay.Models;

namespace QuanLyBanGiay.Controllers
{
    public class CartController : BaseController
    {
        public GioHang? gioHang { get; set; }

        private readonly QLBanGiayDBContext _context;

        public CartController(QLBanGiayDBContext context)
        {
            _context = context;
        }

        // GET: Cart
        public IActionResult Index()
        {
            if(!IsLogin)
            {
                return RedirectToAction("IsLogin", "KHAccount");
            }
            var user = _context.TaiKhoanKHs.FirstOrDefault(m => m.TaiKhoan == CurrentUser);
            if (user != null)
            {
                var gh = HttpContext.Session.GetJson<GioHang>("Cart") ?? new GioHang();
                return View(gh);
            }
            return View();    
        }

        public  IActionResult ThemGioHang(int productID)
        {
            SanPham? sp = _context.SanPhams.FirstOrDefault(m => m.ID == productID);
            if(sp != null)
            {
                gioHang = HttpContext.Session.GetJson<GioHang>("Cart") ?? new GioHang();
                gioHang.ThemMatHang(sp, 1);
                HttpContext.Session.SetJson("Cart", gioHang);
            }
            return View(nameof(Index), gioHang);
        }

        public IActionResult GiamGioHang(int productID)
        {
            SanPham? sp = _context.SanPhams.FirstOrDefault(m => m.ID == productID);
            if (sp != null)
            {
                gioHang = HttpContext.Session.GetJson<GioHang>("Cart") ?? new GioHang();
                
                gioHang.ThemMatHang(sp, -1);
                HttpContext.Session.SetJson("Cart", gioHang);
            }
            return View(nameof(Index), gioHang);
        }

        public IActionResult Xoa(int productID)
        {
            SanPham? sp = _context.SanPhams.FirstOrDefault(m => m.ID == productID);
            if (sp != null)
            {
                gioHang = HttpContext.Session.GetJson<GioHang>("Cart");
                gioHang.XoaMatHang(sp);
                HttpContext.Session.SetJson("Cart", gioHang);
            }
            return View(nameof(Index), gioHang);

        }

        public IActionResult TongTien()
        {
            gioHang = HttpContext.Session.GetJson<GioHang>("Cart");
            gioHang.TinhTongTien();
            HttpContext.Session.SetJson("Cart", gioHang);
            return View(nameof(Index), gioHang);
        }

        public IActionResult XoaTatCa()
        {
            gioHang = HttpContext.Session.GetJson<GioHang>("Cart");
            gioHang.XoaGioHang();
            HttpContext.Session.SetJson("Cart", gioHang);
            return View(nameof(Index), gioHang);
        }

        public IActionResult DatHang(IFormCollection collect, TaiKhoanKH kh)
        {
            
            var user = _context.KhachHangs.FirstOrDefault(m => m.TaiKhoanID == kh.ID);
            if(user == null) 
            {
                return RedirectToAction("Login", "KHAccount");
            }
            GioHang gh = HttpContext.Session.GetJson<GioHang>("Cart") ?? new GioHang();
            DonHang dh = new DonHang();
            dh.KhachHangID = user.ID;
            dh.NgayDatHang = DateTime.Now;
            _context.HoaDons.Add(dh);
            _context.SaveChanges();

            ChiTietDonHang details = new ChiTietDonHang();
            details.HoaDonID = dh.ID;
            details.SoLuongSP = gh.matHangs.Count();
            details.DonGia = gh.TinhTongTien();
            _context.ChiTietDonHangs.Add(details);
            _context.SaveChanges();

            return RedirectToAction("Index", "Home");

        }

        [HttpGet]
        public IActionResult DatHang()
        {
            if (CurrentUser == "")
            {
                return RedirectToAction("Login", "KHAccount");
            }
            var giohang = HttpContext.Session.GetJson<GioHang>("Cart");
            if (giohang == null)
            {
                return RedirectToAction("Shop", "Home");
            }
            ViewBag.User = CurrentUser;
            ViewBag.TongThanhToan = giohang.TinhTongTien();
            ViewBag.SoLuong = giohang.matHangs.Count();
            return View(giohang);
        }

        private bool DonHangExists(int id)
        {
          return (_context.HoaDons?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
