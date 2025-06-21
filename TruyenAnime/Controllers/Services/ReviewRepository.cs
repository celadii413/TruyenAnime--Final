using TruyenAnime.Controllers.Interfaces;
using TruyenAnime.Data;
using TruyenAnime.Models;
using Microsoft.EntityFrameworkCore;

namespace TruyenAnime.Controllers.Services
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly TruyenAnimeDbContext _context;

        public ReviewRepository(TruyenAnimeDbContext context)
        {
            _context = context;
        }

        public void AddReview(Review review)
        {
            review.CreatedAt = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        public IEnumerable<Review> GetReviewsByProductId(int productId)
        {
            return _context.Reviews
                .Where(r => r.ProductId == productId)
                .OrderByDescending(r => r.CreatedAt)
                .ToList();
        }
        public IEnumerable<Review> GetAllReviews()
        {
            return _context.Reviews.Include(r => r.Product).OrderByDescending(r => r.CreatedAt).ToList();
        }

        public Review? GetById(int id)
        {
            return _context.Reviews.Include(r => r.Product).FirstOrDefault(r => r.Id == id);
        }

        public void DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review != null)
            {
                _context.Reviews.Remove(review);
                _context.SaveChanges();
            }
        }
        public IEnumerable<ReviewSummary> GetReviewSummaries()
        {
            return _context.Reviews
                .Include(r => r.Product)
                .GroupBy(r => new { r.ProductId, r.Product.Name, r.Product.ImageUrl })
                .Select(g => new ReviewSummary
                {
                    ProductId = g.Key.ProductId,
                    ProductName = g.Key.Name,
                    ImageUrl = g.Key.ImageUrl,
                    TotalReviews = g.Count()
                })
                .OrderByDescending(r => r.TotalReviews)
                .ToList();
        }

    }
}
