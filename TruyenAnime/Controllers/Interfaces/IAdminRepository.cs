using TruyenAnime.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface IAdminRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int? id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
        Task<bool> ProductExistsAsync(int id);
        Task<bool> CategoryExistsAsync(int categoryId); 
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderDetailsAsync(int? id);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<ContactMessage>> GetAllContactMessagesAsync();
        Task DeleteContactMessageAsync(int id);
        Task<ContactMessage> GetContactMessageByIdAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task DeleteUserAsync(int id);
        Task UpdateUserAsync(User user);
        Task<Category?> GetCategoryByIdAsync(int id);
        Task AddCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);


    }
}