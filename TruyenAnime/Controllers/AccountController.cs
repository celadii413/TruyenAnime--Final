using TruyenAnime.Interfaces; 
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TruyenAnime.Models;


namespace TruyenAnime.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository; 
        private const string SessionUserId = "UserId";

        public AccountController(IAccountRepository accountRepository) 
        {
            _accountRepository = accountRepository;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            ViewData["Title"] = "Đăng nhập";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password) 
        {
            if (TempData["RegisterSuccess"] != null)
            {
                ViewBag.Success = "Đăng ký thành công! Bạn có thể đăng nhập.";
            }

            var user = await _accountRepository.AuthenticateUserAsync(username, password); 

            if (user != null)
            {
                HttpContext.Session.SetInt32(SessionUserId, user.Id);
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("IsAdmin", user.Role == "Admin" ? "true" : "false");
                TempData["SuccessMessage"] = "Đăng nhập thành công!";

                if (user.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Sai tên đăng nhập hoặc mật khẩu!";
            return View();
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            ViewData["Title"] = "Đăng ký";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string email) 
        {
            if (await _accountRepository.IsUsernameTakenAsync(username)) 
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại!";
                return View();
            }

            var newUser = await _accountRepository.RegisterUserAsync(username, password, email); 

            TempData["SuccessMessage"] = "Đăng ký thành công!";
            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionUserId);
            HttpContext.Session.Remove("UserRole"); 
            HttpContext.Session.Remove("IsAdmin"); 
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/MyAccount
        public async Task<IActionResult> MyAccount()
        {
            ViewData["Title"] = "Tài khoản của tôi";
            int? userId = HttpContext.Session.GetInt32(SessionUserId);
            if (userId == null) return RedirectToAction("Login");

            var user = await _accountRepository.GetUserByIdAsync(userId.Value);
            if (user == null) return RedirectToAction("Login");

            var orders = await _accountRepository.GetUserOrdersAsync(userId.Value);

            ViewBag.Orders = orders;
            return View(user); 
        }


        // GET: /Account/EditAddress
        public IActionResult EditAddress()
        {
            ViewData["Title"] = "Địa chỉ";
            return View();
        }
        [HttpGet]
        public IActionResult ChangePassword()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            ViewData["Title"] = "Đổi mật khẩu";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            if (!ModelState.IsValid) return View(model);

            bool result = await _accountRepository.ChangePasswordAsync(userId.Value, model.CurrentPassword, model.NewPassword);

            if (result)
            {
                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("MyAccount");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Mật khẩu hiện tại không đúng!");
                return View(model);
            }
        }

    }
}