using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Repo;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
{
    public class CartService:ICartService
    {
        private readonly ICartRepository _cartrepository;
        public CartService(ICartRepository cartrepository)
        {
            _cartrepository = cartrepository;
        }

        public async Task AddCartItemAsync(string userId, int productId, int quantity)
        {
            var cart=await _cartrepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new Cart { UserId = userId, CartItems = new List<CartItem>() };
                await _cartrepository.AddCartAsync(cart);
            }
            var existing=cart.CartItems.FirstOrDefault(ci=>ci.ProductId==productId);
            if (existing != null)
            {
                existing.Quantity += quantity;
                await _cartrepository.UpdateCartItemAsync(existing);
            }
            else
            {
                var cartItem = new CartItem { CartId = cart.Id, ProductId = productId, Quantity = quantity };
                await _cartrepository.AddCartItemAsync(cartItem);
            }

            await _cartrepository.SaveAsync();



        }

        public async Task<Cart> GetCartByUserIdAsync(string userId)
        {
            var result=await _cartrepository.GetCartByUserIdAsync(userId);
            if (result == null)
            {
                throw new Exception("User dont have Cart Yet");
            }
            return result;
        }

        public async Task<bool> RemoveCartItemAsync(int cartItemId, string userId)
        {
            var cart = await _cartrepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
               throw new Exception("Cart not fount to this User");
            }

            _cartrepository.RemoveCartItemAsync(cartItemId);
            await _cartrepository.SaveAsync();

            return true;
        }
    }
}
