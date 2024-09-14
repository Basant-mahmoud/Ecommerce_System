using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(CategoryDto categoryDto);
        Task<Category> UpdateCategoryAsync(int id ,CategoryDto categoryDto);
        Task DeleteCategoryAsync(int id);
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> GetCategoryByNameAsync(string name);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
    }
}
