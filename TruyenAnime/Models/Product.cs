using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace TruyenAnime.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        public string? Name { get; set; }

        public string? Author { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Tồn kho không được âm")]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        [Required]
        [Range(1900, 2100, ErrorMessage = "Năm xuất bản không hợp lệ")]
        public int PublishYear { get; set; }

        public string? Publisher { get; set; }
        public string? Size { get; set; }

        public bool IsNewArrival { get; set; }
        public bool IsBestSeller { get; set; }
        public bool IsHotDeal { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        [ValidateNever]
        public ICollection<Review> Reviews { get; set; }
    }

}
