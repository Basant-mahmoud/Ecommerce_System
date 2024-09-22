using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDBContext _dbContext; 
        public OrderRepository(EcommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddOrderItemAsync(OrderItem orderItem)
        {
            await _dbContext.OrderItems.AddAsync(orderItem);
           await _dbContext.SaveChangesAsync();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync(); 
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await _dbContext.Orders
                .Include(c => c.OrderItems)
                .ThenInclude(ci => ci.Product)
                .ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
{
    return await _dbContext.Orders
        .Include(o => o.OrderItems) 
        .ThenInclude(oi => oi.Product) 
        .Where(o => o.UserId == userId) 
        .ToListAsync(); 
}


        public async Task <bool> RemoveOrderAsync(int orderId)
        {
            var order = await _dbContext.Orders
        .Include(o => o.OrderItems) 
        .FirstOrDefaultAsync(o => o.Id == orderId); 

            if (order != null)
            {
                _dbContext.Orders.Remove(order);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
                _dbContext.OrderItems.Update(orderItem);
                await _dbContext.SaveChangesAsync();
   
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
