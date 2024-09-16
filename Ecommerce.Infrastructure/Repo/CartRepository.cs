using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationUser _use;
        private readonly EcommerceDBContext _dbContext;
        public CartRepository(ApplicationUser use, EcommerceDBContext dbContext)
        {
            _use = use;
            _dbContext = dbContext;
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _dbContext.Carts.AddAsync(cart);

        }

        public async Task AddCartItemAsync(CartItem cartItem)
        {
            await _dbContext.CartItems.AddAsync(cartItem);
        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            return await _dbContext.Carts
             .Include(c => c.CartItems)
             .ThenInclude(ci => ci.Product)
             .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task RemoveCartItemAsync(int cartItemId)
        {
            var cartitem=await _dbContext.CartItems.FindAsync(cartItemId);
            if (cartitem != null)
            {
                _dbContext.CartItems.Remove(cartitem);
            }
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            _dbContext.CartItems.Update(cartItem);
        }
    }
}
