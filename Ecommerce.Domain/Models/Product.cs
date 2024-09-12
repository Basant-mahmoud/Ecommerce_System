using System.ComponentModel.DataAnnotations;

namespace Ecommerce_System.Ecommerce.Domain.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code {  get; set; }
        public decimal Price { get; set; }
        public decimal Oldprice { get; set; }
        public int StockQuanlity {  get; set; }
        public string ImageUrl { get; set;}
        //forign key
        public int CategoryId { get; set; }
        //navigation property
        public virtual Category Category { get; set; }
        public virtual  ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }

}
