using TruyenAnime.Infrastructure;
using TruyenAnime.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks; 
using TruyenAnime.Interfaces; 

namespace TruyenAnime.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly ICheckoutRepository _checkoutRepository; 
        private const string CartSessionKey = "Cart";

        public CheckoutController(ICheckoutRepository checkoutRepository) 
        {
            _checkoutRepository = checkoutRepository;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetJson<Models.Cart>(CartSessionKey);
            if (cart == null || !cart.Items.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            var order = new Order();
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId.HasValue)
            {

            }
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Name,Address,PhoneNumber,Email")] Order order) 
        {
            var cart = HttpContext.Session.GetJson<Models.Cart>(CartSessionKey);
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null || userId <= 0)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để đặt hàng!";
                return RedirectToAction("Login", "Account");
            }
            if (cart == null || !cart.Items.Any())
            {
                ModelState.AddModelError("", "Giỏ hàng của bạn trống. Vui lòng thêm sản phẩm trước khi thanh toán.");
            }

            if (ModelState.IsValid)
            {
                order.UserId = userId.Value; 
                order.OrderPlaced = System.DateTime.Now;
                order.OrderTotal = cart.GetTotal();
                order.OrderDetails = new List<OrderDetail>();

                foreach (var item in cart.Items)
                {
                    var orderDetail = new OrderDetail
                    {
                        ProductId = item.Product.Id,
                        Quantity = item.Quantity,
                        Price = item.Product.Price
                    };
                    order.OrderDetails.Add(orderDetail);
                }

                var placedOrder = await _checkoutRepository.PlaceOrderAsync(order); 

                HttpContext.Session.Remove(CartSessionKey);

                TempData["OrderId"] = placedOrder.OrderId; 

                return RedirectToAction("Completed");
            }

            return View(order);
        }

        public IActionResult Completed()
        {
            ViewBag.OrderId = TempData["OrderId"];
            if (ViewBag.OrderId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}