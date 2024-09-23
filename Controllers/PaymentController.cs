using Ecommerce_System.Ecommerce.Application.Helper;
using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_System.Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("CreatePayment")]
        [Authorize(Roles = "User,Admin")]

        public async Task<IActionResult> CreatePayment([FromBody] PaymnetDto paymentDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var createdPayment = await _paymentService.CreateAsync(paymentDto);
                return Ok(createdPayment);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetPaymentById")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetPaymentById(int PaymentId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var payment = await _paymentService.GetPaymentByIdAsync(PaymentId);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetAllPayments")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllPayments()
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var payments = await _paymentService.GetAllPaymentAsync();
                return Ok(payments);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("DeletePayment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePayment(int PaymentId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                await _paymentService.DeleteAsync(PaymentId);
                return Ok("Deleted successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("UpdatePayment")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdatePayment( [FromBody] PaymnetDto paymentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var updatedPayment = await _paymentService.UpdatePaymentAsync(paymentDto);
                return Ok(updatedPayment);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetPaymentByOrderId")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPaymentByOrderId(int orderId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();

                }
                var payment = await _paymentService.GetOrderPaymentAsync(orderId);
                return Ok(payment);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
