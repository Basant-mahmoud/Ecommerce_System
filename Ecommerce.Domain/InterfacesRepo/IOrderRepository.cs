using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByUserIdAsync(string userId);
        Task <Order>AddOrderAsync(Order order);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> RemoveOrderItemAsync(int orderItemId);
        Task<IEnumerable<Order>> GetAllOrderAsync();
    }
}
