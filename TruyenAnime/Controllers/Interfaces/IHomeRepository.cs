using TruyenAnime.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Product>> GetLatestProductsAsync(int count);
        Task<IEnumerable<Product>> GetFilteredProductsAsync(string filter, int count);
        Task<Banner> GetBannerAsync();


    }
}