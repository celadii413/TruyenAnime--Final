using TruyenAnime.Models;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface ICartRepository
    {
        Task<Product> GetProductByIdAsync(int productId);

    }
}