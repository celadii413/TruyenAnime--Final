using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic; 
using System.Linq; 
using System.Threading.Tasks; 
using TruyenAnime.Controllers.Interfaces;
using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;

namespace TruyenAnime.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly TruyenAnimeDbContext _context;

        public ProductController(IProductRepository productRepository, IReviewRepository reviewRepository, TruyenAnimeDbContext context) 
        {
            _productRepository = productRepository;
            _reviewRepository = reviewRepository;
            _context = context;
        }

        // GET: /Product/
        // GET: /Product/Index
        public async Task<IActionResult> Index(int? categoryId, string filterType, string searchTerm, int page = 1)
        {
            int pageSize = 9;

            var products = await _productRepository.GetFilteredProductsAsync(categoryId, filterType, searchTerm);
            var totalProducts = products.Count();

            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.Categories = await _productRepository.GetAllCategoriesAsync();
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedFilterType = filterType;
            ViewBag.SearchTerm = searchTerm;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            return View(pagedProducts);
        }



        // GET: /Product/Detail/{id}
        public IActionResult Detail(int id)
        {
            var product = _productRepository.GetProductDetailsAsync(id).Result;
            if (product == null)
                return NotFound();

            var reviews = _reviewRepository.GetReviewsByProductId(id);
            ViewBag.Reviews = reviews;

            if (reviews != null && reviews.Any())
            {
                ViewBag.AvgRating = reviews.Average(r => r.Rating);
            }
            else
            {
                ViewBag.AvgRating = 0;
            }

            return View(product);
        }


        [HttpGet]
        public JsonResult GetSuggestions(string term)
        {
            var suggestions = _context.Products
                .Where(p => EF.Functions.Like(p.Name, $"%{term}%"))
                .Select(p => p.Name)
                .Distinct()
                .Take(5)
                .ToList();

            return Json(suggestions);
        }

    }
}