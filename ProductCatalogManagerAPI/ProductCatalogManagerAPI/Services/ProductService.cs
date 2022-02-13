using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Models;

namespace ProductCatalogManagerAPI.Services
{
    public interface IProductService
    {
        /// <summary>
        /// Fetch all products
        /// </summary>
        Task<IEnumerable<Product>> GetProducts();

        /// <summary>
        /// Fetch specific product
        /// </summary>
        Task<Product> GetProduct(Guid guid);
        /// <summary>
        /// Add new product
        /// </summary>
        Task<Guid> AddProduct(Product request);
        /// <summary>
        /// Delete existing product
        /// </summary>
        Task<int> DeleteProduct(Guid id);
        /// <summary>
        /// Update existing product
        /// </summary>
        Task<Guid> UpdateProduct(Product request);
    }
    public class ProductService : IProductService
    {
        private readonly ProductContext _context;
        public ProductService(ProductContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return _context.Products;
        }

        public async Task<Product> GetProduct(Guid guid)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == guid);
            if (product == null)
                throw new ArgumentNullException();
            return product;
        }

        public async Task<Guid> AddProduct(Product request)
        {
            await _context.Products.AddAsync(request);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
            }
            return request.Id;
        }

        public async Task<int> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
                throw new ArgumentNullException();

            _context.Products.Remove(product);

            try
            {
                return await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                return await _context.SaveChangesAsync();
            }
        }

        public async Task<Guid> UpdateProduct(Product request)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == request.Id);
            if (product == null)
                throw new ArgumentNullException();

            product.Name = request.Name;
            product.Price = request.Price;
            product.Picture = request.Picture;
            product.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
            }

            return product.Id;
        }

    }
}
