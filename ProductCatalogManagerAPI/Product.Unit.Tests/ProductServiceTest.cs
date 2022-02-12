using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFrameworkCoreMock;
using Microsoft.EntityFrameworkCore;
using ProductCatalogManagerAPI.Database;
using ProductCatalogManagerAPI.Models;
using ProductCatalogManagerAPI.Services;
using Moq;
using System.Linq;
using System;
using ProductCatalogManagerAPI.Extensions;

namespace ProductTest.Unit.Tests
{
    public class ProductServiceTest
    {
        public DbContextOptions<ProductContext> DummyOptions { get; } = new DbContextOptionsBuilder<ProductContext>().Options;

        List<Product> initialValues = new List<Product>()
            {
                SeedData.firstProduct,
                SeedData.secondProduct,
                SeedData.thirdProduct
            };

        [Fact]
        public async Task GetProducts_OK()
        {
            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products,initialValues);


            var productService = new ProductService(dbContextMock.Object);

            var products = await productService.GetProducts();

            Assert.IsType<List<Product>>(products.ToList());
        }

        [Fact]
        public async Task GetProduct_OK()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products,initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var guid = new Guid("0448C4E5-6256-4400-89FB-605A6820EC09");

            var product = await productService.GetProduct(guid);

            Assert.IsType<Product>(product);

        }

        [Fact]
        public async Task GetProduct_NotFound()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var guid = new Guid();


            await Assert.ThrowsAsync<ArgumentNullException>(async () => await productService.GetProduct(guid));
        }

        [Fact]
        public async Task AddProduct_OK()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var product = new Product
            {
                Code = "0000",
                Name = "TestProduct",
                Price = 123,
                Picture = "www.test.com"
            };

            var response = await productService.AddProduct(product);

            Assert.Equal(product.Id,response);
        }

        [Fact]
        public async Task AddProduct_AlreadyExist()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var product = new Product
            {
                Code = "666",
                Name = "TestProduct",
                Price = 123,
                Picture = "www.test.com"
            };

            await Assert.ThrowsAsync<ArgumentException>(async () => await productService.AddProduct(product));
        }

        [Fact]
        public async Task DeleteProduct_OK()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var guid = new Guid("5E7BE964-BDBF-438D-AA79-FB324E1AE09E");

            var response = await productService.DeleteProduct(guid);

            Assert.True(response >= 0);

        }

        [Fact]
        public async Task DeleteProduct_NotFound()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var guid = new Guid();

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await productService.DeleteProduct(guid));

        }

        [Fact]
        public async Task UpdateProduct_NotFound()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var guid = new Guid();
            var product = new Product
            {
                Id = guid,
                Code = "000",
                Name = "Test",
                Price = 100
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () => await productService.UpdateProduct(product));
        }

        [Fact]
        public async Task UpdateProduct_OK()
        {

            var dbContextMock = new DbContextMock<ProductContext>(DummyOptions);
            var usersDbSetMock = dbContextMock.CreateDbSetMock(x => x.Products, initialValues);

            var productService = new ProductService(dbContextMock.Object);

            var guid = new Guid("0448C4E5-6256-4400-89FB-605A6820EC09");
            var product = new Product
            {
                Id = guid,
                Code = "666",
                Name = "Test",
                Price = 100
            };

            var response = await productService.UpdateProduct(product);

            Assert.Equal(guid, response);

        }
    }
}
