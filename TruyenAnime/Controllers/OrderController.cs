using Microsoft.AspNetCore.Mvc;
using TruyenAnime.Models; 
using System.Linq; 
using System.Threading.Tasks; 
using TruyenAnime.Interfaces; 

namespace TruyenAnime.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository; 
        private const string SessionUserId = "UserId";

        public OrderController(IOrderRepository orderRepository) 
        {
            _orderRepository = orderRepository;
        }

        // GET: /Order/MyOrders
        public async Task<IActionResult> MyOrders() 
        {
            int? userId = HttpContext.Session.GetInt32(SessionUserId);
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để xem đơn hàng của mình.";
                return RedirectToAction("Login", "Account");
            }

            var orders = await _orderRepository.GetUserOrdersAsync(userId.Value);
            return View(orders);
        }

        // GET: /Order/Detail/{id}
        public async Task<IActionResult> Detail(int id) 
        {
            int? userId = HttpContext.Session.GetInt32(SessionUserId);
            if (userId == null)
            {
                TempData["ErrorMessage"] = "Bạn cần đăng nhập để xem chi tiết đơn hàng.";
                return RedirectToAction("Login", "Account");
            }

            var order = await _orderRepository.GetOrderDetailsAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            if (order.UserId != userId.Value)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền truy cập đơn hàng này.";
                return RedirectToAction("MyOrders"); 
            }

            return View(order);
        }
    }
}