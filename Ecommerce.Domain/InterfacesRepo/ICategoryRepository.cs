using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category category);
        Task<Category> UpdateAsync(Category category);
        Task<bool> DeleteAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllAsync();


    }
}
