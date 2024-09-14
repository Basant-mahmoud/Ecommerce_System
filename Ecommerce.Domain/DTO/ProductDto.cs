namespace Ecommerce_System.Ecommerce.Domain.DTO
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Price { get; set; }
        public decimal Oldprice { get; set; }
        public int StockQuanlity { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
