using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<Category> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var exist=await _categoryRepository.GetByNameAsync(categoryDto.Name.ToLower());
            if (exist != null) {
                throw new Exception($"category '{categoryDto.Name}' aready exist");
            }
            
            var category = new Category
            {
                Name = categoryDto.Name.ToLower(),
            };
            var createdCategory=await _categoryRepository.AddAsync(category);
            if (createdCategory == null) {
                throw new Exception($"cant add this category plz try again.");
            }
            return createdCategory;

        }

        public async Task DeleteCategoryAsync(int id)
        {
            var exist= await _categoryRepository.GetByIdAsync(id);
            if (exist == null)
            {
                throw new Exception($"category not exist.");

            }
            var isDeleted = await _categoryRepository.DeleteAsync(id);
            if (!isDeleted)
            {
                throw new Exception("Failed to delete the category, please try again.");
            }
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            var found= await _categoryRepository.GetByIdAsync(id);
            if (found == null) {
                throw new Exception($"category not exist.");
            }
            return found;
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var exist= await _categoryRepository.GetByNameAsync(name.ToLower());
            if (exist == null)
            {
                throw new Exception($"category not exist.");
            }
            return exist;
        }

        public async Task<Category> UpdateCategoryAsync(int id ,CategoryDto categoryDto)
        {

            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                throw new Exception("Category not found.");
            }

            category.Name = categoryDto.Name.ToLower(); // Update the category name

            var updatedCategory = await _categoryRepository.UpdateAsync(category);
            if (updatedCategory == null)
            {
                throw new Exception("Failed to update the category, please try again.");
            }
            return updatedCategory; // Return the updated category

        }
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

    }
}
