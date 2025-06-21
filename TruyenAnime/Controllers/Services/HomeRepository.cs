using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyenAnime.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public HomeRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetLatestProductsAsync(int count)
        {
            return await _context.Products
                                 .Include(p => p.Category) 
                                 .OrderByDescending(p => p.Id) 
                                 .Take(count) 
                                 .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(string filter, int count)
        {
            var query = _context.Products.AsQueryable();

            switch (filter)
            {
                case "new":
                    query = query.Where(p => p.IsNewArrival);
                    break;
                case "bestseller":
                    query = query.Where(p => p.IsBestSeller);
                    break;
                case "hot":
                    query = query.Where(p => p.IsHotDeal);
                    break;
                case "all":
                default:
                    break;
            }

            return await query
                .OrderByDescending(p => p.Id)
                .Take(count)
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<Banner> GetBannerAsync()
        {
            return await _context.Banners.FirstOrDefaultAsync(); 
        }


    }
}