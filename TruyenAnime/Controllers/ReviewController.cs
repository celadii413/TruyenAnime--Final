using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TruyenAnime.Controllers.Interfaces;
using TruyenAnime.Models;

namespace TruyenAnime.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewController(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        [HttpPost]
        public IActionResult Add(Review review)
        {
            if (review.ProductId <= 0 || string.IsNullOrEmpty(review.Username) || string.IsNullOrEmpty(review.Content))
            {
                return BadRequest("Missing data");
            }

            _reviewRepository.AddReview(review);
            return RedirectToAction("Detail", "Product", new { id = review.ProductId });
        }
        
    }
}
