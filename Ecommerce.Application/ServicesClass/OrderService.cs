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
        private readonly IProductRepository _productRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }
        public async Task AddOrderItemAsync(string userId, AddItemDto orderItem)
        {
            // Fetch orders by user ID
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            // Check if the order exists and belongs to the user
            var order = orders.FirstOrDefault(o => o.Id == orderItem.orderid);
            if (order == null)
            {
                throw new Exception("Order ID doesn't belong to this user.");
            }

            // Fetch product by ID
            var product = await _productRepository.GetByIdAsync(orderItem.productId);
            if (product == null)
            {
                throw new Exception("Product ID is not correct.");
            }

            // Create a new OrderItem
            var newOrderItem = new OrderItem
            {
                OrderId = order.Id,
                ProductId = orderItem.productId,
                Quantity = orderItem.quantity,
                Price = product.Price 
            };

            // Add the new order item
            await _orderRepository.AddOrderItemAsync(newOrderItem);

            // Update product stock quantity
            product.StockQuanlity -= orderItem.quantity;
            await _productRepository.UpdateAsync(product);

            // Update the total order amount
            order.TotalAmount += orderItem.quantity * product.Price;
            await _orderRepository.UpdateOrderAsync(order);
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

        public async Task<IEnumerable<OrderDto>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
            {
                throw new Exception("User doesn't have any orders yet");
            }

            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown",
                    Quantity = oi.Quantity,
                    Price = oi.Product?.Price ?? 0
                }).ToList()
            }).ToList();
        }

        public async Task<bool> RemoveOrderItemAsync(string Userid, int orderId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(Userid);
            var order = orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                throw new Exception("Order ID doesn't belong to this user.");
            }

           bool result = await _orderRepository.RemoveOrderAsync(orderId);

            if (result==false)
            {
                throw new Exception("Can't remove this order item. Please try again.");
            }
            return true;
        }

        public async Task UpdateOrderItemAsync(string userId, UpdateOrderItemDto updateOrder)
        {
            // Fetch orders by user ID
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);

            // Check if the order exists and belongs to the user
            var order = orders.FirstOrDefault(o => o.Id == updateOrder.orderId);
            if (order == null)
            {
                throw new Exception("Order ID doesn't belong to this user.");
            }

            // Validate that the product exists
            var product = await _productRepository.GetByIdAsync(updateOrder.ProductId);
            if (product == null)
            {
                throw new Exception("Product ID is not correct.");
            }

            // Check if the order item exists in the order
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == updateOrder.orderitemId);
            if (orderItem == null)
            {
                throw new Exception("order item not in this product ");

            }
            else
            {
                // Update the existing order item
                orderItem.Quantity = updateOrder.Quantity;
                orderItem.ProductId = updateOrder.ProductId;

                // Update the order item in the repository
                await _orderRepository.UpdateOrderItemAsync(orderItem);
            }

            // Adjust product stock after adding or updating an order item
            product.StockQuanlity -= updateOrder.Quantity;
            await _productRepository.UpdateAsync(product);
            order.TotalAmount= updateOrder.Quantity*product.Price;
            await _orderRepository.UpdateOrderAsync(order);
        }


    }
}
