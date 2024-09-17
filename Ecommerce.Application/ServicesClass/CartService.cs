using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task AddCartItemAsync(string userId, int productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                throw new Exception("we dont have this product in system");
            };
            if (product.StockQuanlity < quantity)
            {
                throw new Exception("Quantity you need not available in store");
            }
            // Get the cart for the user, if it exists
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Create a new cart if none exists
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = new List<CartItem>()
                };
                await _cartRepository.AddCartAsync(cart);
                await _cartRepository.SaveAsync(); 
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (existingCartItem != null)
            {
                // Update the quantity if the item is already in the cart
                existingCartItem.Quantity += quantity;
                await _cartRepository.UpdateCartItemAsync(existingCartItem);
            }
            else
            {
                var cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _cartRepository.AddCartItemAsync(cartItem);
            }
            // Update stock quantity
            product.StockQuanlity -= quantity;
            await _productRepository.UpdateAsync(product);
            await _cartRepository.SaveAsync();
        }

        public async Task<CartDto> GetCartByUserIdAsync(string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found for this user.");
            }

            var totalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity);
            // mapping to avoid cycle
            return new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price
                }).ToList(),
                TotalAmount = totalAmount 
            };
        }
        public async Task<bool> RemoveCartItemAsync(int cartItemId, string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                throw new Exception("Cart not found for this user.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found.");
            }

            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            product.StockQuanlity += cartItem.Quantity;
            await _productRepository.UpdateAsync(product);

            await _cartRepository.RemoveCartItemAsync(cartItemId);
            await _cartRepository.SaveAsync();

            return true;
        }
        public async Task<IEnumerable<CartDto>> GetAllCartsAsync()
        {
            var carts = await _cartRepository.GetAllCartAsync();
            if (carts == null)
            {
                throw new Exception("Not have Carts Yet");
            }
            return carts.Select(cart => new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CartItems = cart.CartItems.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price
                }).ToList(),
                TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity) 
            }).ToList();
            
        }
        public async Task<bool> DecreaseCartItemQuantityAsync(int cartItemId, string userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                throw new Exception("Cart not found for this user.");
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (cartItem == null)
            {
                throw new Exception("Cart item not found.");
            }
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);

            if (cartItem.Quantity > 1)
            {
                cartItem.Quantity -= 1;
                product.StockQuanlity += 1;
                await _cartRepository.UpdateCartItemAsync(cartItem);
                await _productRepository.UpdateAsync(product);
            }
            else
            {
                await _cartRepository.RemoveCartItemAsync(cartItemId);
            }

            await _cartRepository.SaveAsync();
            return true;
        }


    }
}
