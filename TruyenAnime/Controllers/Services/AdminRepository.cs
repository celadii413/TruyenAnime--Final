using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyenAnime.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public AdminRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int? id)
        {
            return await _context.Products
                .Include(p => p.Category) 
                .FirstOrDefaultAsync(p => p.Id == id);
        }


        public async Task AddProductAsync(Product product)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            var existing = await _context.Products.FindAsync(product.Id);
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


        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Products.AnyAsync(e => e.Id == id);
        }

        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.User).OrderByDescending(o => o.OrderPlaced).ToListAsync();
        }

        public async Task<Order> GetOrderDetailsAsync(int? id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(m => m.OrderId == id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync()
        {
            return await _context.ContactMessages
                .OrderByDescending(c => c.SentDate)
                .ToListAsync();
        }

        public async Task DeleteContactMessageAsync(int id)
        {
            var contact = await _context.ContactMessages.FindAsync(id);
            if (contact != null)
            {
                _context.ContactMessages.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ContactMessage> GetContactMessageByIdAsync(int id)
        {
            return await _context.ContactMessages.FindAsync(id);
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }


    }
}