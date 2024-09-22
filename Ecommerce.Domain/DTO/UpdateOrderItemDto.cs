namespace Ecommerce_System.Ecommerce.Domain.DTO
{
    public class UpdateOrderItemDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int  orderId { get; set; }
        public int orderitemId {  get; set; }
    }
}
