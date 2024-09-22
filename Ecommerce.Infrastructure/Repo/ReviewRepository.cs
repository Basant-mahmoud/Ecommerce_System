using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly EcommerceDBContext _dbContext;
        public ReviewRepository(EcommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Review> AddReviewAsync(Review reviw)
        {
            _dbContext.Reviews.Add(reviw);
            await _dbContext.SaveChangesAsync();
            return reviw;
        
        }

        public async Task<Review> DeleteReviewAsync(Review reviw)
        {
           
                _dbContext.Reviews.Remove(reviw);
                await _dbContext.SaveChangesAsync();
            return reviw;
        }
        public async Task<Review> UpdateReviwAsync(Review reviw)
        {
           _dbContext.Update(reviw);
            await _dbContext.SaveChangesAsync();
            return reviw;
        }
        public async Task<IEnumerable<Review>> GetAllProductReviewAsync(int ProductId)
        {
            return await _dbContext.Reviews
                .Where(review => review.ProductId == ProductId)
                .ToListAsync();
        }


        public Task<Review> GetUserReviwAsync(string userId, int ProductId)
        {
            throw new NotImplementedException();
        }

        public async Task<Review> GetReviewByIdAsync(int ReviewId)
        {
            var review=await _dbContext.Reviews.FindAsync(ReviewId);
            return review;
        }
    }
}
