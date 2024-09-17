using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface ICartService
    {
        Task<CartDto> GetCartByUserIdAsync(string userId);
        Task AddCartItemAsync(string userId, int productId, int quantity);
        Task<bool> RemoveCartItemAsync(int cartItemId, string userId);
        Task<IEnumerable<CartDto>> GetAllCartsAsync();
        Task<bool> DecreaseCartItemQuantityAsync(int cartItemId, string userId);

    }
}
