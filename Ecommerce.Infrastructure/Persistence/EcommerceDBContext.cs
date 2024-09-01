using Ecommerce_System.Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Persistence
{
    public class EcommerceDBContext : IdentityDbContext<ApplicationUser>
    {
    }
}
