using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce_System.Ecommerce.Infrastructure.Repo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly EcommerceDBContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthRepository(EcommerceDBContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;   
        }

        public async Task<bool> EmailExistAsync(string email) { 
        
                return await _userManager.FindByEmailAsync(email) != null;
        }
        public async Task<bool> UsernameExistsAsync(string username) { 
        
                    return await _userManager.FindByNameAsync(username) != null;
        }
    }
}
