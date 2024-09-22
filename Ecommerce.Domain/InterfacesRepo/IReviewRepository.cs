using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IReviewRepository
    {
        Task<Review>AddReviewAsync(Review reviw);
        Task<Review> DeleteReviewAsync(Review reviw);
        Task<Review> UpdateReviwAsync(Review reviw);
        Task<IEnumerable<Review>> GetAllProductReviewAsync(int ProductId);
        Task<Review> GetUserReviwAsync(string userId,int ProductId);
        Task <Review> GetReviewByIdAsync(int ReviewId);
    }
}
