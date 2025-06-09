using ProductManagerApi.Models;

namespace ProductManagerApi.Services
{
    public interface IProductService
    {
        Task<List<ProductItem>> GetAllProductsAsync();
        Task<ProductItem?> GetProductByIdAsync(int id);
        Task<ProductItem> CreateProductAsync(ProductItem Product);
        Task<bool> UpdateProductAsync(int id, ProductItem updatedProduct);
        Task<bool> DeleteProductAsync(int id);
    }
}