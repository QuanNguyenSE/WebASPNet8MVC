using Microsoft.AspNetCore.Mvc;
using WebASPNet8MVC.Helpers;
using WebASPNet8MVC.ViewModels;

namespace WebASPNet8MVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>
                (MySetting.CART_KEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModels
            {
                Quantity = cart.Sum(p => p.SoLuong),
                Total = cart.Sum(p => p.ThanhTien),
            });
        }
    }
}
