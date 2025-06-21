using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyenAnime.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public ProductRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(int? categoryId, string filterType)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable(); 

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value);
            }

            if (!string.IsNullOrEmpty(filterType))
            {
                switch (filterType.ToLower())
                {
                    case "new":
                        products = products.Where(p => p.IsNewArrival);
                        break;
                    case "bestseller":
                        products = products.Where(p => p.IsBestSeller);
                        break;
                    case "hotdeal":
                        products = products.Where(p => p.IsHotDeal);
                        break;
                }
            }
            return await products.ToListAsync(); // Thực thi query và trả về List
        }

        public async Task<Product> GetProductDetailsAsync(int productId)
        {
            return await _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetFilteredProductsAsync(int? categoryId, string filterType, string searchTerm)
        {
            var products = _context.Products.Include(p => p.Category).AsQueryable();

            if (categoryId.HasValue)
                products = products.Where(p => p.CategoryId == categoryId.Value);

            if (!string.IsNullOrEmpty(filterType))
            {
                switch (filterType.ToLower())
                {
                    case "new":
                        products = products.Where(p => p.IsNewArrival);
                        break;
                    case "bestseller":
                        products = products.Where(p => p.IsBestSeller);
                        break;
                    case "hotdeal":
                        products = products.Where(p => p.IsHotDeal);
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.Name.Contains(searchTerm));
            }

            return await products.ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var existing = await _context.Products.FirstOrDefaultAsync(p => p.Id == product.Id);
            if (existing != null)
            {
                existing.Name = product.Name;
                existing.Author = product.Author;
                existing.Description = product.Description;
                existing.Price = product.Price;
                existing.Stock = product.Stock;
                existing.ImageUrl = product.ImageUrl;
                existing.CategoryId = product.CategoryId;
                existing.PublishYear = product.PublishYear;
                existing.Publisher = product.Publisher; 
                existing.Size = product.Size;
                existing.IsNewArrival = product.IsNewArrival;
                existing.IsBestSeller = product.IsBestSeller;
                existing.IsHotDeal = product.IsHotDeal;

                await _context.SaveChangesAsync();
            }
        }


        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}