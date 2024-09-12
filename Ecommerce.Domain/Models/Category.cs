namespace Ecommerce_System.Ecommerce.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product>Products { get; set; }//navigation property
    }
}
