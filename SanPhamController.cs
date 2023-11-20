using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Models;

namespace QuanLyBanGiay.Controllers
{
    public class SanPhamController : BaseController
    {
        private readonly QLBanGiayDBContext _context;

        public SanPhamController(QLBanGiayDBContext context)
        {
            _context = context;
        }

        // GET: SanPham
        public IActionResult Index()
        {
            ViewBag.User = CurrentUser;
            var sanpham = _context.SanPhams.Select(m => new SanPham()
            {
                ID = m.ID,
                TenGiay = m.TenGiay,
                GiaBan = m.GiaBan,
                HinhAnh = m.HinhAnh,
                PhanLoai = m.PhanLoai,
                ThuongHieu = m.ThuongHieu,
                MauSac = m.MauSac,
            });
            
             
            return View(sanpham);
        }

        // GET: SanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // GET: SanPham/Create
        public IActionResult Create(int id)
        {
            return View(new SanPham() { ID = 0});
        }

        public string UploadImage(int id, IFormFile img)
        {
            var path = Path.GetFullPath("./wwwroot/images/");
            var imageName = Path.GetFileName(img.FileName);

            var filePath = string.Format("{0}/{1}", path, imageName);
            using Stream stream = new FileStream(filePath, FileMode.Create);
            img.CopyTo(stream);
            return "/images/" + imageName;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SanPham sanPham, IFormFile img)
        {
            try
            {
                sanPham.HinhAnh = UploadImage(sanPham.ID, img);
                _context.Add(sanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(sanPham);
            }
            
        }

        // GET: SanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham == null)
            {
                return NotFound();
            }
            return View(sanPham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SanPham sanPham)
        {
            if (id != sanPham.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SanPhamExists(sanPham.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sanPham);
        }

        // GET: SanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SanPhams == null)
            {
                return NotFound();
            }

            var sanPham = await _context.SanPhams
                .FirstOrDefaultAsync(m => m.ID == id);
            if (sanPham == null)
            {
                return NotFound();
            }

            return View(sanPham);
        }

        // POST: SanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SanPhams == null)
            {
                return Problem("Entity set 'QLBanGiayDBContext.SanPhams'  is null.");
            }
            var sanPham = await _context.SanPhams.FindAsync(id);
            if (sanPham != null)
            {
                _context.SanPhams.Remove(sanPham);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SanPhamExists(int id)
        {
            return (_context.SanPhams?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
