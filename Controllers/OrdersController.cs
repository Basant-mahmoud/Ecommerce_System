using Ecommerce_System.Ecommerce.Application.Helper;
using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
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
        [HttpGet("GetOderByUserId")]
        public async Task<IActionResult> GetOderByUserId()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var order = await _orderService.GetOrdersByUserIdAsync(userId);
                return Ok(order);

            }
            catch (Exception ex)
            {
                return NotFound(new {message=ex.Message});
            }
        }
        [HttpDelete("RemoveOrder")]
        public async Task<IActionResult> RemoveOrder([FromBody] RemoveOrderDto removeorder)
        {
            try
            {
                /*var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }*/
                var order = await _orderService.RemoveOrderItemAsync(removeorder.UserId, removeorder.OrderId);
                return Ok("Order Deleted Successfly");

            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateOrderItem")]
        public async Task<IActionResult> UpdateOrderItem( [FromBody] UpdateOrderItemDto updateorder)
        {
            try
            {
                var userId = User.GetUserId(); 
                if (userId == null)
                {
                    return Unauthorized();
                }

                await _orderService.UpdateOrderItemAsync(userId, updateorder);
                return Ok("Order item updated successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem([FromBody] AddItemDto orderitem)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }

                await _orderService.AddOrderItemAsync(userId, orderitem);
                return Ok("Order item Added successfully.");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
   
    
}
