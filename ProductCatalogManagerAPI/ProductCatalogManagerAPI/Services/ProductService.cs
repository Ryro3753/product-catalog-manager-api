﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Models;

namespace ProductCatalogManagerAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(Guid guid);
        Task<Guid> AddProduct(Product request);
        Task DeleteProduct(Guid id);
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

        public async Task DeleteProduct(Guid id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
                throw new ArgumentNullException();

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
            }
        }
    }
}
