using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.Models;

namespace Ecommerce_System.Ecommerce.Application.InterfacesServices
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> CreateProductAsync(ProductDto product);
        Task<Product> UpdateProductAsync(int id , ProductDto product);
        Task<bool>DeleteProductAsync(int id);
        Task<Product> GetById(int id);
    }
}
