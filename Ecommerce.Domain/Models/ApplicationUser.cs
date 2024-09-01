using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce_System.Ecommerce.Domain.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
    
    }
}
