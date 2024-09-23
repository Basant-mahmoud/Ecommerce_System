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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("AddReview")]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> AddReview([FromBody] AddReviewDto review)
        {
            try
            {
                var userId = User.GetUserId();  
                if (userId == null)
                {
                    return Unauthorized();
                }

                var result = await _reviewService.AddReviewAsync(review, userId);

                return Ok(result); 
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });  
            }
        }
        [HttpDelete("DeleteReview")]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> DeleteReview(int ReviewId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var Role = User.GetUserRole();
                await _reviewService.DeleteReviewAsync(userId,Role, ReviewId);

                return Ok("Review Deleted Successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("GetAllProductReview")]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> GetAllProductReview(int productId)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
               var result= await _reviewService.GetAllProductReviewsAsync(productId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpGet("GetReviewById")]
        [Authorize(Roles = "User ,Admin")]
        public async Task<IActionResult> GetReviewById(int reviewid)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _reviewService.GetReviewByIdAsync(reviewid);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("UpdateReview")]
        [Authorize(Roles = "User , Admin")]
        public async Task<IActionResult> UpdateReview(UpdateReviewDto updatereview)
        {
            try
            {
                var userId = User.GetUserId();
                if (userId == null)
                {
                    return Unauthorized();
                }
                var result = await _reviewService.UpdateReviwAsync(userId,updatereview);

                return Ok("Review Updated Successfly");
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }



    }
}
