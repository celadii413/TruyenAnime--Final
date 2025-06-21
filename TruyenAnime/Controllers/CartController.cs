using Microsoft.AspNetCore.Mvc;
using TruyenAnime.Models;
using TruyenAnime.Infrastructure; 
using System.Linq;
using System.Threading.Tasks; 
using TruyenAnime.Interfaces; 

namespace TruyenAnime.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository; 
        private const string CartSessionKey = "Cart";

        public CartController(ICartRepository cartRepository) 
        {
            _cartRepository = cartRepository;
        }

        // GET: /Cart/
        // GET: /Cart/Index
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<Models.Cart>(CartSessionKey) ?? new Models.Cart();

            if (cart.Items == null || !cart.Items.Any())
            {
                return RedirectToAction("EmptyCart");
            }

            ViewBag.CartCount = cart.Items.Sum(i => i.Quantity);
            return View(cart);
        }


        [HttpPost]
        public async Task<IActionResult> AddToCart(int id, int quantity = 1) 
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng!";
                return RedirectToAction("Login", "Account");
            }

            var product = await _cartRepository.GetProductByIdAsync(id);
            if (product != null)
            {
                var cart = HttpContext.Session.GetJson<Models.Cart>(CartSessionKey) ?? new Models.Cart();
                cart.AddItem(product, quantity);
                HttpContext.Session.SetJson(CartSessionKey, cart);

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    int cartCount = cart.Items.Sum(i => i.Quantity);
                    return Json(new { cartCount });
                }
            }
            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            var cart = HttpContext.Session.GetJson<Models.Cart>(CartSessionKey);
            if (cart != null)
            {
                cart.RemoveItem(id);
                HttpContext.Session.SetJson(CartSessionKey, cart);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetJson<Models.Cart>(CartSessionKey);
            if (cart != null)
            {
                cart.UpdateItem(id, quantity);
                HttpContext.Session.SetJson(CartSessionKey, cart);
            }
            return RedirectToAction("Index");
        }
        public IActionResult EmptyCart()
        {
            return View();
        }

    }
}