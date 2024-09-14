using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpPost("Create Category")]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryDto category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (category == null)
            {
                return BadRequest("name must be not null");
            }
            var createdCategory = await _categoryService.CreateCategoryAsync(category);
            if (createdCategory == null)
            {
                return NotFound("cant add this category plz try again");
            }
            return Ok(createdCategory);
        }
        [HttpGet("GetByid{id:int}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var category = await _categoryService.GetCategoryByIdAsync(id);
                return Ok(category);

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPost("GetByName")]
        public async Task<IActionResult> GetCategoryByName([FromBody] CategoryDto categ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var category = await _categoryService.GetCategoryByNameAsync(categ.Name);
                return Ok(category);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
      [HttpPost("updateCategory")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryDto categ)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updated = await _categoryService.UpdateCategoryAsync(id, categ);
                if (updated==null)
                {
                    return NotFound();
                }

                return Ok(new { message = "Category updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        
       [HttpDelete("DeleteCtegory")]
       public async Task<IActionResult> DeleteCategory(int id)
       {
           if (!ModelState.IsValid)
           {
               return BadRequest(ModelState);
           }
           try
           {
               await _categoryService.DeleteCategoryAsync(id);
               return Ok(new { message = "Category deleted successfully." });
           }
           catch (Exception ex)
           {
               return NotFound(new {message=ex.Message});
           }
       }
        
       [HttpGet("GetAllCategory")]
       public async Task<IActionResult> GetAllCategory()
       {
           try
           {
               var categories = await _categoryService.GetAllCategoriesAsync();
               return Ok(categories);
           }
           catch (Exception ex)
           {
               return NotFound(new {message=ex.Message});
           }
       }
    }
}
