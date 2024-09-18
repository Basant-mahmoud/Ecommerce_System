using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string Userid);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<Order> GetOrderByUserIdAsync(string userId);
        Task<bool> RemoveOrderItemAsync(int orderItemId);
        Task UpdateOrderItemAsync(OrderItem orderItem);
    }
}
