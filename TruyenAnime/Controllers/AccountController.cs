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

            ViewBag.Error = "Sai tên đăng nhập/Email hoặc mật khẩu!";
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

            if (!IsValidEmail(email))
            {
                ViewBag.Error = "Địa chỉ email không hợp lệ!";
                return View();
            }

            if (!email.EndsWith("@gmail.com"))
            {
                ViewBag.Error = "Email phải có đuôi @gmail.com!";
                return View();
            }

            var newUser = await _accountRepository.RegisterUserAsync(username, password, email);

            TempData["SuccessMessage"] = "Đăng ký thành công!";
            return RedirectToAction("Login");
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; 
            return System.Text.RegularExpressions.Regex.IsMatch(email, emailRegex);
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
        public async Task<IActionResult> ChangeInfo()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            var user = await _accountRepository.GetUserByIdAsync(userId.Value);
            if (user == null) return RedirectToAction("Login");

            var model = new ChangeInfo
            {
                Username = user.Username,
                Email = user.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeInfo(ChangeInfo model)
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");

            if (!ModelState.IsValid) return View(model);

            var user = await _accountRepository.GetUserByIdAsync(userId.Value);
            if (user == null) return RedirectToAction("Login");

            if (user.Password != model.CurrentPassword)
            {
                ModelState.AddModelError("CurrentPassword", "Mật khẩu hiện tại không đúng!");
                return View(model);
            }

            user.Username = model.Username;
            user.Email = model.Email;

            if (!string.IsNullOrWhiteSpace(model.NewPassword))
            {
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp!");
                    return View(model);
                }

                user.Password = model.NewPassword;
            }

            await _accountRepository.SaveChangesAsync();

            TempData["SuccessMessage"] = "Cập nhật tài khoản thành công!";
            return RedirectToAction("MyAccount");
        }


    }
}