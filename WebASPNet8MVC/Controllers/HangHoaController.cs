using Microsoft.AspNetCore.Mvc;
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
    }
}
