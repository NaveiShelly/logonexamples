using Microsoft.EntityFrameworkCore;
using ProductManagerApi.Data;
using ProductManagerApi.Models;

namespace ProductManagerApi.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductItem>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductItem?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<ProductItem> CreateProductAsync(ProductItem Product)
        {
            _context.Products.Add(Product);
            await _context.SaveChangesAsync();
            return Product;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductItem updatedProduct)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null) return false;

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            existingProduct.InStock = updatedProduct.InStock;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var Product = await _context.Products.FindAsync(id);
            if (Product == null) return false;

            _context.Products.Remove(Product);
            await _context.SaveChangesAsync();

            return true;
        }

    
    }
}
