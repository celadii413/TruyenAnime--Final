using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TruyenAnime.Models
{
    public class Order
    {
        public int UserId { get; set; } // Thêm trường UserId để liên kết với User
        public virtual User? User { get; set; } // Navigation property
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên của bạn")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ giao hàng")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        public List<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public decimal OrderTotal { get; set; }
        public System.DateTime OrderPlaced { get; set; }
    }
}
