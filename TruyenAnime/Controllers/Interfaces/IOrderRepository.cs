using TruyenAnime.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TruyenAnime.Interfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Lấy tất cả các đơn hàng của một người dùng cụ thể.
        /// </summary>
        /// <param name="userId">ID của người dùng.</param>
        /// <returns>Danh sách các đơn hàng của người dùng.</returns>
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId);

        /// <summary>
        /// Lấy chi tiết một đơn hàng cụ thể theo ID đơn hàng.
        /// </summary>
        /// <param name="orderId">ID của đơn hàng.</param>
        /// <returns>Đối tượng Order nếu tìm thấy, ngược lại là null.</returns>
        Task<Order> GetOrderDetailsAsync(int orderId);
    }
}