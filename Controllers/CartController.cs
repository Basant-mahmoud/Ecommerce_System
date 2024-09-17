using Ecommerce_System.Ecommerce.Application.Helper;
using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecommerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("GetUserCart")]
        public async Task<IActionResult> GetUserCart() 
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return  Unauthorized();
                }

                var cart = await _cartService.GetCartByUserIdAsync(userId);
                if (cart == null)
                {
                    return NotFound("User dont have cart");
                }

                return Ok(cart);

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem(AddItemDto item)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                await _cartService.AddCartItemAsync(userId, item.productId, item.quantity);
                return Ok("Product add to Cart Successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
           
        }

        [HttpDelete]
        [Route("RemoveItem/{cartItemId}")]
        public async Task<IActionResult> RemoveItem(int cartItemId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                await _cartService.RemoveCartItemAsync(cartItemId, userId);
                return Ok("Cart item deleted Successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }
        [HttpGet("GetAllCart")]
        public async Task<IActionResult> GetAllCart()
        {
            try
            {
                /*var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }*/
               var carts= await _cartService.GetAllCartsAsync();
               return Ok(carts);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpDelete]
        [Route("DecreaseQuantity/{cartItemId}")]
        public async Task<IActionResult> DecreaseQuantity(int cartItemId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                await _cartService.DecreaseCartItemQuantityAsync(cartItemId, userId);
                return Ok("Decrease Quantity Successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }

        }

    }
}
