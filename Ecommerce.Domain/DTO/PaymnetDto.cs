
namespace Ecommerce_System.Ecommerce.Domain.DTO
{
    public class PaymnetDto
    {
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}