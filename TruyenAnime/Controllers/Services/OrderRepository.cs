using TruyenAnime.Data;
using TruyenAnime.Interfaces;
using TruyenAnime.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TruyenAnime.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public OrderRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderDetails)
                                     .ThenInclude(od => od.Product)
                                 .Where(o => o.UserId == userId)
                                 .OrderByDescending(o => o.OrderPlaced)
                                 .ToListAsync();
        }

        public async Task<Order> GetOrderDetailsAsync(int orderId)
        {
            return await _context.Orders
                                 .Include(o => o.OrderDetails)
                                     .ThenInclude(od => od.Product)
                                 .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }
    }
}