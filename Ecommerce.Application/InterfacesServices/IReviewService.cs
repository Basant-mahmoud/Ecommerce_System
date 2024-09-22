using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface IReviewService
    {

        Task<Review> AddReviewAsync(AddReviewDto reviw, string userid);
        Task DeleteReviewAsync(string UserId,string Role,int ReviewId);
        Task<Review> UpdateReviwAsync(string UserId, UpdateReviewDto updatereview);
        Task<IEnumerable<GetReviewsDto>> GetAllProductReviewsAsync(int ProductId);
        Task<Review> GetReviewByIdAsync(int Reviewid);
    }
}
