using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore;
using QuanLyBanGiay.Models;

namespace QuanLyBanGiay.Controllers
{
    public class KHAccountController : BaseController
	{
		private readonly QLBanGiayDBContext _context;

		public KHAccountController(QLBanGiayDBContext context)
		{
			_context = context;
		}

		public IActionResult IsLogin()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(TaiKhoanKH tk)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var loginUser = await _context.TaiKhoanKHs.FirstOrDefaultAsync(m => m.TaiKhoan == tk.TaiKhoan);
			if (loginUser == null)
			{
				ModelState.AddModelError("", "Đăng nhập thất bại");
				return View(tk);
			}
			else
			{
				SHA256 hashMethod = SHA256.Create();
				if (Util.Cryptography.VerifyHash(hashMethod, tk.MatKhau, loginUser.MatKhau))
				{
					CurrentUser = loginUser.TaiKhoan;
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Đăng nhập thất bại");
					return View(tk);
				}
			}


		}

        public IActionResult AdLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdLogin(string username, string password)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var loginUser = await _context.Admins.FirstOrDefaultAsync(m => m.TaiKhoanAdmin == username);
            if (loginUser == null)
            {
                ModelState.AddModelError("", "Đăng nhập thất bại");
                return View();
            }
            else
            {
                CurrentUser = loginUser.TaiKhoanAdmin;
                return RedirectToAction("Index", "SanPham");
                
            }
        }

        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register([Bind("ID,TaiKhoan,MatKhau")] TaiKhoanKH tk)
		{
			if (ModelState.IsValid)
			{
				SHA256 hashMethod = SHA256.Create();
				tk.MatKhau = Util.Cryptography.GetHash(hashMethod, tk.MatKhau);
				_context.Add(tk);
				await _context.SaveChangesAsync();
				return RedirectToAction("Information", "KHAccount");
			}
			else
			{
				ModelState.AddModelError("", "Đăng ký thất bại");
				return View(tk);
			}
		}

		public IActionResult Information()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Information(KhachHang kh)
		{
			try
			{
                var tk = _context.TaiKhoanKHs.FirstOrDefault(m => m.ID == kh.TaiKhoanID);
                if (tk == null)
                {
                    return NotFound();
                }
                _context.Add(kh);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Profile");
            }
			catch
			{
				return View(kh);
			}

        }

        public IActionResult Logout()
		{
			CurrentUser = "";
			return RedirectToAction("Index", "Home");
		}
	}
}
