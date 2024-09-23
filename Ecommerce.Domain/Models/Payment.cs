using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Ecommerce_System.Ecommerce.Domain.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [JsonIgnore]// to avoid cycle insteed of make dto
        public Order Order { get; set; }
        public string PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

    }
}
