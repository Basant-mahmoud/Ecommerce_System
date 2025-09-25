using Ecommerce_System.Ecommerce.Application.Helper;
using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost("AddProduct")]
        [Authorize(Roles = "User , Admin")]

        public async Task<IActionResult> AddProduct([FromBody]ProductDto product)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var result = await _productService.CreateProductAsync(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("GetAllProduct")]
        [Authorize(Roles = "Admin , User")]

        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var result = await _productService.GetAllProductAsync();
                if (result == null)
                {
                    return NotFound("dont have product in system yet");
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
           

        }
        [HttpPost("UpdateProduct")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult>UpdateProduct(int id, [FromBody] ProductDto product)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var result = await _productService.UpdateProductAsync(id,product);
                return Ok("product Updated successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new {message=ex.Message});
            }
        }
        [HttpDelete("DeleteProduct")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult>DeleteProduct(int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                await _productService.DeleteProductAsync(id);
                return Ok("Product Deleted Successfly");
            }
            catch(Exception ex)
            {
                return NotFound(new {message=ex.Message});
            }
        }
        [HttpGet("GetProductById")]
        [Authorize(Roles = "Admin , User")]
        public async Task<IActionResult>GetProductById(int id)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var result= await _productService.GetById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
               return NotFound(new {message=ex.Message});
            }
        }
    }
}
