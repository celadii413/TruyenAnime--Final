using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TruyenAnime.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public CartRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _context.Products.FindAsync(productId);
        }

    }
}