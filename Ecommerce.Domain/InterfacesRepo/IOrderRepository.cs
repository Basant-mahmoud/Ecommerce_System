using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task <Order>AddOrderAsync(Order order);
        Task AddOrderItemAsync(OrderItem orderItem);
        Task UpdateOrderItemAsync(OrderItem orderItem);
        Task<bool> RemoveOrderAsync(int orderId);
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<Order>GetOrderByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);
        Task SaveAsync();
    }
}
