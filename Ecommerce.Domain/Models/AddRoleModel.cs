using System.ComponentModel.DataAnnotations;

namespace Ecommerce_System.Ecommerce.Domain.Models
{
    public class AddRoleModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
