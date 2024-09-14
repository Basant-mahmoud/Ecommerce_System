using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class ProductRepository : IProductRepository
    {
        private readonly EcommerceDBContext _dbContext;
        public ProductRepository(EcommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> AddAsync(Product product)
        {
             await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _dbContext.Products.FindAsync(id);
            if (result != null)
            {
                _dbContext.Products.Remove(result);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
           return await _dbContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Products.FindAsync(id);
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;

        }
    }
}
