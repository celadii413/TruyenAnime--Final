using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Linq; 
using System.Threading.Tasks; 
using TruyenAnime.Data;
using TruyenAnime.Infrastructure; 
using TruyenAnime.Interfaces; 
using TruyenAnime.Models;

namespace TruyenAnime.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHomeRepository _homeRepository;
    private readonly TruyenAnimeDbContext _context;


    public HomeController(ILogger<HomeController> logger, IHomeRepository homeRepository, TruyenAnimeDbContext context) 
    {
        _logger = logger;
        _homeRepository = homeRepository;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _homeRepository.GetLatestProductsAsync(10);

        var cart = HttpContext.Session.GetJson<Models.Cart>("Cart");
        int cartCount = cart != null ? cart.Items.Sum(i => i.Quantity) : 0;
        ViewBag.CartCount = cartCount;

        var banner = await _homeRepository.GetBannerAsync();

        if (banner == null)
        {
            banner = new Banner
            {
                Title = "Truyện mới tháng 6",
                ButtonText = "Mua ngay",
                BackgroundColor = "#f8f9fa"
            };
        }

        ViewBag.Banner = banner;

        return View(products);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public async Task<IActionResult> FilteredProducts(string filter = "all")
    {
        var products = await _homeRepository.GetFilteredProductsAsync(filter, 10);

        var banner = await _homeRepository.GetBannerAsync();
        if (banner == null)
        {
            banner = new Banner
            {
                Title = "Truyện mới tháng 6",
                ButtonText = "Mua ngay",
                BackgroundColor = "#f8f9fa"
            };
        }
        ViewBag.Banner = banner;

        var cart = HttpContext.Session.GetJson<Cart>("Cart");
        int cartCount = cart?.Items.Sum(i => i.Quantity) ?? 0;
        ViewBag.CartCount = cartCount;

        ViewBag.CurrentFilter = filter;

        return View("Index", products); 
    }


}