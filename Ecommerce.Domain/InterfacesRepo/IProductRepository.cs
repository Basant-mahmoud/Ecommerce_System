using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Domain.InterfacesRepo
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> AddAsync(Product product);
    }
}
