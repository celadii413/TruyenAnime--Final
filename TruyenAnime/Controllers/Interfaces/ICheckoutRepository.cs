using TruyenAnime.Models;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface ICheckoutRepository
    {
        /// <summary>
        /// Lưu một đơn hàng mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="order">Đối tượng Order cần lưu.</param>
        /// <returns>Đối tượng Order đã được lưu với ID đã cập nhật.</returns>
        Task<Order> PlaceOrderAsync(Order order);
    }
}