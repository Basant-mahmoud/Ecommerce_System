using System.Text.Json.Serialization;

namespace Ecommerce_System.Ecommerce.Domain.Models
{
    public class Review
    {
       public  int Id { get; set; }
        public int ProductId { get; set; }
        [JsonIgnore] //This will prevent the Product property from being serialized, breaking the cycle
        public virtual Product Product { get; set; }
        public string UserId {  get; set; }
        public virtual ApplicationUser User { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
