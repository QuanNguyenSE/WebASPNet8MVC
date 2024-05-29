using Microsoft.AspNetCore.Mvc;
using WebASPNet8MVC.Data;
using WebASPNet8MVC.Helpers;
using WebASPNet8MVC.ViewModels;

namespace WebASPNet8MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly HshopContext _context;

        public CartController(HshopContext context)
        {
            _context = context;
        }

        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>
            (MySetting.CART_KEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }
        public IActionResult AddToCart(int? Id, int quantity = 1)
        {
            var giohang = Cart;
            var item = giohang.FirstOrDefault(p => p.MaHh == Id);
            if (item == null)
            {
                var data = _context.HangHoas.FirstOrDefault(p => p.MaHh == Id);
                if (data == null)
                {
                    TempData["Message"] = $"Không tìm thấy mã hàng hóa {Id}";
                    return Redirect("/404");

                }
                item = new CartItem
                {
                    MaHh = data.MaHh,
                    TenHh = data.TenHh,
                    Hinh = data.Hinh,
                    DonGia = data.DonGia ?? 0,
                    SoLuong = quantity

                };
                giohang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(MySetting.CART_KEY, giohang);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveCart(int? id)
        {
            var giohang = Cart;
            var item = giohang.FirstOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var data = _context.HangHoas.FirstOrDefault(p => p.MaHh == id);
                if (data == null)
                {
                    TempData["Message"] = $"Không tìm thấy mã hàng hóa {id}";
                    return Redirect("/404");

                }

            }
            giohang.Remove(item);

            HttpContext.Session.Set(MySetting.CART_KEY, giohang);
            return RedirectToAction("Index");
        }
    }
}
