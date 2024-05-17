using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebASPNet8MVC.Data;
using WebASPNet8MVC.ViewModels;

namespace WebASPNet8MVC.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly HshopContext _context;

        public HangHoaController(HshopContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? loai)
        {
            var data = _context.HangHoas.AsQueryable();
            if (loai != null)
            {
                data = data.Where(p => p.MaLoai == loai);
            }
            var res = data.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                Hinh = p.Hinh ?? "",
                DonGia = p.DonGia ?? 0,
                MoTaDonVi = p.MoTaDonVi,
                TenLoai = p.MaLoaiNavigation.TenLoai,
            });
            return View(res);
        }
        public IActionResult Search(string? query)
        {
            var data = _context.HangHoas.AsQueryable();
            if (query != null)
            {
                data = data.Where(p => p.TenHh.Contains(query));
            }
            var res = data.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                Hinh = p.Hinh ?? "",
                DonGia = p.DonGia ?? 0,
                MoTaDonVi = p.MoTaDonVi,
                TenLoai = p.MaLoaiNavigation.TenLoai,
            });
            return View(res);
        }
        public IActionResult Detail(int? id)
        {
            var data = _context.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (data == null)
            {
                TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
                return Redirect("/404");
            }

            var result = new ChiTietHangHoaVM
            {
                MaHh = data.MaHh,
                TenHH = data.TenHh,
                DonGia = data.DonGia ?? 0,
                ChiTiet = data.MoTa ?? string.Empty,
                Hinh = data.Hinh ?? string.Empty,
                MoTaNgan = data.MoTaDonVi ?? string.Empty,
                TenLoai = data.MaLoaiNavigation.TenLoai,
                SoLuongTon = 10,//tính sau
                DiemDanhGia = 5,//check sau
            };

            return View(result);
        }
    }
}
