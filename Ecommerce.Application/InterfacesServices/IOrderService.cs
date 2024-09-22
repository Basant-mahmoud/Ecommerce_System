using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;
using System.Collections.Generic;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(string Userid);
        Task AddOrderItemAsync(string userId, AddItemDto orderItem);
        Task<IEnumerable<OrderDto>> GetAllOrdersAsync();
        Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId);
        Task<bool> RemoveOrderItemAsync(string Userid,int orderId);
        Task UpdateOrderItemAsync(string userId, UpdateOrderItemDto updateorder);
    }
}
