using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Repo;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }
        public Task AddOrderItemAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public async Task<OrderDto> CreateOrderAsync(string userId)
        {
            try
            {
                var cart = await _cartRepository.GetCartByUserIdAsync(userId);
                if (cart == null || !cart.CartItems.Any())
                {
                    throw new Exception("Cannot create order, cart is empty.");
                }

                // Create a new order based on the cart
                var order = new Order
                {
                    UserId = cart.UserId,
                    OrderDate = DateTime.Now,
                    OrderItems = cart.CartItems.Select(ci => new OrderItem
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                        Product = ci.Product 
                    }).ToList(),
                    TotalAmount = cart.CartItems.Sum(ci => ci.Product.Price * ci.Quantity)
                };

                var storedOrder = await _orderRepository.AddOrderAsync(order);

                
                cart.CartItems.Clear();
                await _cartRepository.SaveAsync();

                return new OrderDto
                {
                    Id = storedOrder.Id,
                    OrderDate = storedOrder.OrderDate,
                    UserId = storedOrder.UserId,
                    OrderItems = storedOrder.OrderItems.Select(ci => new OrderItemDto
                    {
                        Id = ci.Id,
                        ProductId = ci.ProductId,
                        ProductName = ci.Product.Name,
                        Quantity = ci.Quantity,
                        Price = ci.Product.Price
                    }).ToList(),
                    TotalAmount = storedOrder.TotalAmount
                };
            }
            catch (Exception ex)
            {
             
                throw new Exception("An error occurred while creating the order.", ex);
            }
        }


        public async Task<IEnumerable<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllOrderAsync();
            if (orders == null || !orders.Any())
            {
                throw new Exception("No orders available yet.");
            }

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                UserId = order.UserId,
                OrderItems = order.OrderItems.Select(ci => new OrderItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity,
                    ProductName = ci.Product.Name,
                    Price = ci.Product.Price
                }).ToList(),
                TotalAmount = order.OrderItems.Sum(ci => ci.Product.Price * ci.Quantity)
            }).ToList();
        }

        public async Task<OrderDto> GetOrderByUserIdAsync(string userId)
        {
            var order = await _orderRepository.GetOrderByUserIdAsync(userId);
            if (order == null)
            {
                throw new Exception("User doesn't have any orders yet");
            }

            return new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown", // Use a default value if Product is null
                    Quantity = oi.Quantity,
                    Price = oi.Product?.Price ?? 0 // Use a default value if Price is null
                }).ToList()
            };
        }


        public Task<bool> RemoveOrderItemAsync(int orderItemId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
