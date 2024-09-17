using Ecommerce_System.Ecommerce.Application.InterfacesServices;
using Ecommerce_System.Ecommerce.Domain.DTO;
using Ecommerce_System.Ecommerce.Domain.InterfacesRepo;
using Ecommerce_System.Ecommerce.Domain.Models;
using Ecommerce_System.Ecommerce.Infrastructure.Repo;

namespace Ecommerce_System.Ecommerce.Application.ServicesClass
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productrepository;
        public ProductService(IProductRepository productrepository)
        {
            _productrepository = productrepository;
        }

        public async Task<Product> CreateProductAsync(ProductDto product)
        {
            var created = new Product
            {
                Name = product.Name,
                Description = product.Description,
                StockQuanlity = product.StockQuanlity,
                Code = product.Code,
                Oldprice = product.Oldprice,
                CategoryId = product.CategoryId,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
            };
            var result=await _productrepository.AddAsync(created);
            if (result == null)
            {
                throw new Exception("cant Add this Product plz try again");
            }
            return result;

        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product=await _productrepository.GetByIdAsync(id);
            if (product == null) 
            {
                throw new Exception("this product not found plz try again");
            }
           var delete=await _productrepository.DeleteAsync(id);
            if (delete==false) 
            {
                throw new Exception("can't delete this product");
            }
            return true;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            var result=await _productrepository.GetAllAsync();
            if (result == null)
            {
                throw new Exception("we dont have product in system yet");
            }
            return result;
        }

        public async Task<Product> GetById(int id)
        {
            var result = await _productrepository.GetByIdAsync(id);
            if(result == null)
            {
                throw new Exception($"Product {id} not found");
            }
            return result;
        }

        public async Task<Product> UpdateProductAsync(int id , ProductDto product)
        {
            var existingProduct = await _productrepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found.");
            }

           
            if (!string.IsNullOrEmpty(product.Name)|| product.Name != "string")
            {
                existingProduct.Name = product.Name;
            }

            if (!string.IsNullOrEmpty(product.Description)|| product.Description != "string")
            {
                existingProduct.Description = product.Description;
            }

            if (!string.IsNullOrEmpty(product.Code) || product.Code != "string")
            {
                existingProduct.Code = product.Code;
            }

            if (product.Price != 0)
            {
                existingProduct.Price = product.Price;
            }

            if (product.Oldprice != 0)
            {
                existingProduct.Oldprice = product.Oldprice;
            }

            if (product.StockQuanlity != 0)
            {
                existingProduct.StockQuanlity = product.StockQuanlity;
            }

            if (!string.IsNullOrEmpty(product.ImageUrl)|| product.ImageUrl != "string")
            {
                existingProduct.ImageUrl = product.ImageUrl;
            }

            if (product.CategoryId != 0)
            {
                existingProduct.CategoryId = product.CategoryId;
            }

            var updatedProduct = await _productrepository.UpdateAsync(existingProduct);

            return updatedProduct;
        }
    }
}
