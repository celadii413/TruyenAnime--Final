using Microsoft.AspNetCore.Mvc;

namespace TruyenAnime.Models
{
    public class ReviewSummary
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string? ImageUrl { get; set; }
        public int TotalReviews { get; set; }
    }

}
