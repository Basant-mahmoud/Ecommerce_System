using Ecommerce_System.Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce_System.Ecommerce.Infrastructure.Persistence
{
    public class EcommerceDBContext : IdentityDbContext<ApplicationUser>
    {
        public EcommerceDBContext(DbContextOptions<EcommerceDBContext>options):base(options) 
        { 

        }
       
    }
}
