using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using System.Threading.Tasks;

namespace TruyenAnime.Repositories
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public CheckoutRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PlaceOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}