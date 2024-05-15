using Microsoft.AspNetCore.Mvc;
using WebASPNet8MVC.Data;
using WebASPNet8MVC.ViewModels;

namespace WebASPNet8MVC.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly HshopContext _context;

        public MenuLoaiViewComponent(HshopContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var data = _context.Loais.Select(lo => new MenuLoaiVM
            {
                MaLoai = lo.MaLoai,
                TenLoai = lo.TenLoai,
                SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);

            return View(data);
        }
    }
}
