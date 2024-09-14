using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EcommerceDBContext _dbContext;
        public CategoryRepository(EcommerceDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Category> AddAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category;
        }

        public async Task<bool> DeleteAsync(int id)
        {

            var category = await _dbContext.Categories.FindAsync(id);
            if (category != null)
            {
                _dbContext.Categories.Remove(category);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;

        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _dbContext.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public Task<Category> GetByNameAsync(string name)
        {
            return _dbContext.Categories.FirstOrDefaultAsync(c => c.Name == name.ToLower());
        }

        public async  Task<Category> UpdateAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
            return category;

        }
    }
}
