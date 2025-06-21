using TruyenAnime.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetFilteredProductsAsync(int? categoryId, string filterType);

        Task<Product> GetProductDetailsAsync(int productId);

        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        Task<IEnumerable<Product>> GetFilteredProductsAsync(int? categoryId, string filterType, string searchTerm);

        Task UpdateProductAsync(Product product);

        Task<Product> GetProductByIdAsync(int id);

    }
}