using TruyenAnime.Models;

namespace TruyenAnime.Controllers.Interfaces
{
    public interface IReviewRepository
    {
        void AddReview(Review review);
        IEnumerable<Review> GetReviewsByProductId(int productId);
        IEnumerable<ReviewSummary> GetReviewSummaries(); 
        void DeleteReview(int id);
    }
}
