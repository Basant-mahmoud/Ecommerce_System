using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepository _productRepository;
        public ReviewService(IReviewRepository reviewRepository,IProductRepository productRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }

        public async Task<Review> AddReviewAsync(AddReviewDto reviw,string userid)
        {
            var product= await _productRepository.GetByIdAsync(reviw.ProductId);
            if (product == null)
            {
                throw new Exception("Product not Found");
            }
            var addreview = new Review
            {
                Comment = reviw.Comment,
                ProductId = reviw.ProductId,
                Rating=reviw.Rating,
                ReviewDate = DateTime.Now,
                UserId = userid
            };
           var result= await _reviewRepository.AddReviewAsync(addreview);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new Exception("Cant Added this review plz try again");

            }


        }

        public async Task DeleteReviewAsync(string UserId,string Role, int ReviewId)
        {
            var review=await _reviewRepository.GetReviewByIdAsync(ReviewId);
            if (review == null)
            {
                throw new Exception("Review ID Not Correct");
            }
            if(review.UserId != UserId && Role !="Admin")
            {
                throw new Exception("Cant Delete this Review only User or Admin Can Delete it ");
            }
            var result = await _reviewRepository.DeleteReviewAsync(review);
            if (result == null) 
            {
                throw new Exception("Cant Delete this review plz try again");
            }
           
        }

        public async Task<IEnumerable<GetReviewsDto>> GetAllProductReviewsAsync(int ProductId)
        {
            var product = await _productRepository.GetByIdAsync(ProductId);
            if (product == null)
            {
                throw new Exception("Product not found. Please make sure of Product ID.");
            }

            var reviews = await _reviewRepository.GetAllProductReviewAsync(ProductId);
            if (!reviews.Any())
            {
                throw new Exception("This product doesn't have any reviews yet.");
            }

            var result = reviews.Select(review => new GetReviewsDto
            {
                reviewId = review.Id,
                Rating = review.Rating,
                Comment = review.Comment,
                ReviewDate = review.ReviewDate,
                UserId = review.UserId,
                ProductName = product.Name  
            });

            return result;
        }

        public async Task<Review> GetReviewByIdAsync(int Reviewid)
        {
            var result= await _reviewRepository.GetReviewByIdAsync(Reviewid);
            if (result == null)
            {
                throw new Exception("cant find this review");

            }
            return result;
        }

        public async Task<Review> UpdateReviwAsync(string UserId,UpdateReviewDto updatereview)
        {
            var product=await _productRepository.GetByIdAsync(updatereview.ProductId);
            if(product == null)
            {
                throw new Exception("Product id not correct");
            }
            var review=await _reviewRepository.GetReviewByIdAsync(updatereview.reviewId);
            if (review == null)
            {
                throw new Exception("Review id not correct");
            }
            review.ReviewDate = updatereview.ReviewDate;
            review.UserId = UserId;
            review.ProductId = product.Id;
            review.Rating = updatereview.Rating;
            review.Comment = updatereview.Comment;
            var result = await _reviewRepository.UpdateReviwAsync(review);
            if (result==null)
            {
                throw new Exception("Cant Update this review");
            }
            return result;
        }
    }
}
