using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Models;
using System.Diagnostics;

namespace QuanLyBanGiay.Controllers
{
    public class HomeController : BaseController
    {
        private readonly QLBanGiayDBContext _context;

        public HomeController(QLBanGiayDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var sp = _context.SanPhams.ToList();
            return View(sp);
        }

        public IActionResult Details(int id)
        {
            var sanPham = _context.SanPhams.Where(m => m.ID == id).ToList();
            
            return View(sanPham);
        }

        [HttpGet]
        public async Task<IActionResult> TimKiem(string pname, double? from, double? to)
        {
            if(!string.IsNullOrEmpty(pname))
            {
                if(from != null && to != null)
                {
                    var product = _context.SanPhams.Where(m => m.TenGiay.Contains(pname) && m.GiaBan >= from && m.GiaBan <= to).Select(m => new SanPham()
                    {
                        ID = m.ID,
                        TenGiay = m.TenGiay,
                        HinhAnh = m.HinhAnh,
                        GiaBan = m.GiaBan
                    });
                    return View(nameof(Shop), await product.ToListAsync());
                }
                else 
                {
                    var product = _context.SanPhams.Where(m => m.TenGiay.Contains(pname)).Select(m => new SanPham()
                    {
                        ID = m.ID,
                        TenGiay = m.TenGiay,
                        HinhAnh = m.HinhAnh,
                        GiaBan = m.GiaBan
                    });
                    return View(nameof(Shop), await product.ToListAsync());
                }
            }
            else
            {
                if (from != null && to != null)
                {
                    var product = _context.SanPhams.Where(m => m.GiaBan >= from && m.GiaBan <= to).Select(m => new SanPham()
                    {
                        ID = m.ID,
                        TenGiay = m.TenGiay,
                        HinhAnh = m.HinhAnh,
                        GiaBan = m.GiaBan
                    });
                    return View(nameof(Shop), await product.ToListAsync());
                }
            }
            return View(nameof(Shop));
        }

        //[HttpGet]
        //public async Task<IActionResult> Loc(int? idThuongHieu)
        //{
        //    if(idThuongHieu == null)
        //    {
        //        idThuongHieu = 0;
        //    }
        //    var thuongHieu = _context.ThuongHieus.ToList();

        //    thuongHieu.Insert(0, new ListThuongHieu { ID = 0, TenThuongHieu = "---Chọn Thương Hiệu---" });
        //    ViewBag.ThuongHieuID = new SelectList(thuongHieu, "ID", "TenThuongHieu", idThuongHieu);

        //    var brand = _context.SanPhams.Where(m => m.ThuongHieuID == idThuongHieu);
        //    return View(nameof(Shop), await brand.ToListAsync());
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}