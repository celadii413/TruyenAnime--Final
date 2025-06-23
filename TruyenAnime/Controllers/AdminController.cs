using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using System.Threading.Tasks;
using TruyenAnime.Controllers.Interfaces;
using TruyenAnime.Filters;
using TruyenAnime.Interfaces; 
using TruyenAnime.Models;
using TruyenAnime.Data;

namespace TruyenAnime.Controllers
{
    [AuthorizeAdmin]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly TruyenAnimeDbContext _context; 

        public AdminController(IAdminRepository adminRepository, IReviewRepository reviewRepository, TruyenAnimeDbContext context) 
        {
            _adminRepository = adminRepository;
            _reviewRepository = reviewRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: /Admin/Products
        public async Task<IActionResult> Products(int page = 1)
        {
            int pageSize = 5;

            var allProducts = await _adminRepository.GetAllProductsAsync();
            int totalProducts = allProducts.Count();
            int totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            var pagedProducts = allProducts
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(pagedProducts);
        }

        // GET: Admin/CreateProduct
        public async Task<IActionResult> CreateProduct() 
        {
            ViewBag.Categories = await _adminRepository.GetAllCategoriesAsync(); 
            return View();
        }

        // POST: Admin/CreateProduct
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _adminRepository.AddProductAsync(product); 
                TempData["ProductCreatedMessage"] = "Sản phẩm đã được tạo thành công!";
                return RedirectToAction(nameof(Products));
            }
            ViewBag.Categories = await _adminRepository.GetAllCategoriesAsync(); 
            return View(product);
        }

        // GET: Admin/DeleteProduct/5
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _adminRepository.GetProductByIdAsync(id); 
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/DeleteProduct/5
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            await _adminRepository.DeleteProductAsync(id); 
            TempData["ProductDeletedMessage"] = "Sản phẩm đã được xóa thành công!";

            return RedirectToAction(nameof(Products));
        }

        // GET: /Admin/Orders
        public async Task<IActionResult> Orders()
        {
            var orders = await _adminRepository.GetAllOrdersAsync(); 
            return View(orders);
        }

        // GET: /Admin/OrderDetails/5
        public async Task<IActionResult> OrderDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _adminRepository.GetOrderDetailsAsync(id); 

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: /Admin/Users
        public async Task<IActionResult> Users()
        {
            var users = await _adminRepository.GetAllUsersAsync(); 
            return View(users);
        }

        // GET: /Admin/ContactMessages
        public async Task<IActionResult> ContactMessages()
        {
            var messages = await _adminRepository.GetAllContactMessagesAsync();
            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteContactMessage(int id)
        {
            await _adminRepository.DeleteContactMessageAsync(id);
            TempData["ContactDeletedMessage"] = "Đã xóa tin nhắn liên hệ.";
            return RedirectToAction(nameof(ContactMessages));
        }

        // GET: ContactDetail
        public async Task<IActionResult> ContactDetail(int id)
        {
            var message = await _adminRepository.GetContactMessageByIdAsync(id);
            if (message == null)
            {
                return NotFound();
            }
            return View(message);
        }

        // POST: ReplyContact
        [HttpPost]
        public async Task<IActionResult> ReplyContact(string email, string replyMessage)
        {
            TempData["ContactSentMessage"] = "Phản hồi đã được gửi thành công tới " + email;
            return RedirectToAction("ContactMessages");
        }

        // GET: Admin/EditUser/5
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: Admin/EditUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(int id, [Bind("Id,Username,Email,Role,Password")] User user)
        {
            if (id != user.Id) return NotFound();

            // Gán mặc định nếu null
            user.Reviews ??= new List<Review>();

            ModelState.Remove(nameof(user.Reviews));

            if (ModelState.IsValid)
            {
                await _adminRepository.UpdateUserAsync(user);
                TempData["SuccessMessage"] = "Cập nhật người dùng thành công.";
                return RedirectToAction("Users");
            }

            // Nếu lỗi, log lỗi rõ ràng
            foreach (var entry in ModelState)
            {
                foreach (var error in entry.Value.Errors)
                {
                    Console.WriteLine($"Lỗi tại {entry.Key}: {error.ErrorMessage}");
                }
            }

            return View(user);
        }

        // GET: Admin/EditProduct/5
        [HttpGet]
        public async Task<IActionResult> EditProduct(int? id)
        {
            if (id == null) return NotFound();

            var product = await _adminRepository.GetProductByIdAsync(id);
            if (product == null) return NotFound();

            var categories = await _adminRepository.GetAllCategoriesAsync();
            ViewBag.Categories = categories;

            return View(product);
        }

        // POST: Admin/EditProduct/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> EditProduct(Product product)
        {

            if (!ModelState.IsValid)
            {
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($">>> Lỗi ở '{key}': {error.ErrorMessage}");
                    }
                }

                var categories = await _adminRepository.GetAllCategoriesAsync();
                ViewBag.Categories = categories;
                return View(product);
            }

            Console.WriteLine(">>>>> Model hợp lệ, đang cập nhật DB...");

            await _adminRepository.UpdateProductAsync(product);
            return RedirectToAction("Products");
        }

        // POST: Admin/DeleteUser/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _adminRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            await _adminRepository.DeleteUserAsync(id);
            TempData["UserDeletedMessage"] = "Người dùng đã được xóa thành công.";
            return RedirectToAction("Users");
        }

        public IActionResult ReviewSummary()
        {
            var summaries = _reviewRepository.GetReviewSummaries();
            return View(summaries);
        }

        public IActionResult ProductReviews(int productId)
        {
            var reviews = _reviewRepository.GetReviewsByProductId(productId);
            ViewBag.ProductId = productId;
            return View(reviews);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReview(int reviewId, int productId)
        {
            _reviewRepository.DeleteReview(reviewId);
            TempData["ReviewDeletedMessage"] = "Đánh giá đã được xóa.";
            return RedirectToAction("ProductReviews", new { productId });
        }
        [HttpGet]
        public async Task<IActionResult> Banner()
        {
            var banner = await _context.Banners.FirstOrDefaultAsync();
            if (banner == null)
            {
                banner = new Banner();
            }
            return View(banner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveBanner(Banner banner)
        {
            if (!ModelState.IsValid)
            {
                // Ghi log từng lỗi ra Console
                foreach (var entry in ModelState)
                {
                    var key = entry.Key;
                    var errors = entry.Value.Errors;
                    foreach (var error in errors)
                    {
                        Console.WriteLine($">>> Lỗi ở '{key}': {error.ErrorMessage}");
                    }
                }

                return View("Banner", banner);
            }

            Console.WriteLine(">>>>> Model hợp lệ, đang lưu banner...");

            var existing = await _context.Banners.FirstOrDefaultAsync();
            if (existing != null)
            {
                existing.Title = banner.Title;
                existing.ButtonText = banner.ButtonText;
                existing.BackgroundImageUrl = banner.BackgroundImageUrl;
                existing.BackgroundColor = banner.BackgroundColor;
            }
            else
            {
                _context.Banners.Add(banner);
            }

            await _context.SaveChangesAsync();

            TempData["BannerSavedMessage"] = "Cập nhật banner thành công!";
            return RedirectToAction("Banner");
        }
        // GET: Admin/Categories
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _adminRepository.GetAllCategoriesAsync();
            return View(categories);
        }

        // GET: Admin/CreateCategory
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View(new Category());
        }

        // POST: Admin/CreateCategory
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            if (string.IsNullOrWhiteSpace(category.Name))
            {
                ModelState.AddModelError("Name", "Tên danh mục không được để trống.");
            }

            if (!ModelState.IsValid)
                return View(category);

            await _adminRepository.AddCategoryAsync(category);
            TempData["CategoryCreatedMessage"] = "Thêm danh mục thành công!";
            return RedirectToAction("Categories");
        }

        // GET: Admin/EditCategory/5
        [HttpGet]
        public async Task<IActionResult> EditCategory(int id)
        {
            var category = await _adminRepository.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();

            return View(category);
        }

        // POST: Admin/EditCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCategory(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            await _adminRepository.UpdateCategoryAsync(category);
            TempData["CategoryUpdatedMessage"] = "Cập nhật danh mục thành công!";
            return RedirectToAction("Categories");
        }

        // POST: Admin/DeleteCategory/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _adminRepository.DeleteCategoryAsync(id);
            TempData["CategoryDeletedMessage"] = "Xóa danh mục thành công!";
            return RedirectToAction("Categories");
        }


    }
}