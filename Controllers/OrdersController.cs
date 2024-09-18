using Ecommerce_System.Ecommerce.Application.Helper;
using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet("GetAllOrder")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.GetAllOrdersAsync();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
            
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var createdOrder = await _orderService.CreateOrderAsync(userId);
                return Ok(createdOrder);

            }
            catch (Exception ex)
            {
                return NotFound(new {message=ex.Message});
            }
           
        }
    }
}
