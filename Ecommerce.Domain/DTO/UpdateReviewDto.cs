using Ecommerce_System.Ecommerce.Domain.Models;
using System.Text.Json.Serialization;

namespace Ecommerce_System.Ecommerce.Domain.DTO
{
    public class UpdateReviewDto
    {
        public int reviewId { get; set; }
        public int ProductId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
