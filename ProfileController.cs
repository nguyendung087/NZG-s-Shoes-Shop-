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
    public class ProfileController : BaseController
    {
        private readonly QLBanGiayDBContext _context;

        public ProfileController(QLBanGiayDBContext context)
        {
            _context = context;
        }

        // GET: Profile
        public IActionResult Index()
        {
            if (!IsLogin)
            {
                return RedirectToAction("IsLogin", "KHAccount");
            }
            var thongTin = _context.TaiKhoanKHs.FirstOrDefault(m => m.TaiKhoan == CurrentUser);
            if (thongTin != null)
            {
                int taiKhoanID = thongTin.ID;

                var khachHang = _context.KhachHangs.Where(m => m.TaiKhoanID == taiKhoanID).ToList();
                if (khachHang != null)
                {
                    return View(khachHang);
                }
            }
            return View();
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.KhachHangs == null)
            {
                return NotFound();
            }

            var khachHang = await _context.KhachHangs.FindAsync(id);
            if (khachHang == null)
            {
                return NotFound();
            }
            return View(khachHang);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, KhachHang khachHang)
        {
            if (id != khachHang.ID)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    _context.Update(khachHang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachHangExists(khachHang.ID))
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
        }

       
        private bool KhachHangExists(int id)
        {
          return (_context.KhachHangs?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
