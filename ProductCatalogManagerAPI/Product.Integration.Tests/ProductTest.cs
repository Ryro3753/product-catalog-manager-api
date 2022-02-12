using ProductCatalogManagerAPI.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ProductTest.Integration.Tests
{
    public class ProductTest
    {
        [Fact]
        public async Task AddProduct_OK()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var req = new Product()
            {
                Code = "111",
                Name = "Test",
                Price = 299,
                Picture = "https://picsum.photos/200/300"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/Product", content);

            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_CodeNotFound()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var req = new Product()
            {
                Name = "Test",
                Price = 299,
                Picture = "https://picsum.photos/200/300"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/Product", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_PriceOutOfBounds()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var req = new Product()
            {
                Name = "Test",
                Code = "111",
                Price = 50000,
                Picture = "https://picsum.photos/200/300"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/Product", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_NameNotFound()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var req = new Product()
            {
                Code = "111",
                Price = 299,
                Picture = "https://picsum.photos/200/300"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/Product", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task AddProduct_PictureURLInvalid()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var req = new Product()
            {
                Name = "Test",
                Code = "111",
                Price = 299,
                Picture = "Example"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("/api/v1/Product", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetProducts_OK()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var response = await client.GetAsync("/api/v1/Product");

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProduct_OK()
        {
            var application = new WebApplication();
            var client = application.CreateClient();
            var guid = new Guid("868D54E6-5CFD-450F-BA51-760DF652E6D4");

            var response = await client.GetAsync("/api/v1/Product/" + guid);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetProduct_InvalidGuid()
        {
            var application = new WebApplication();
            var client = application.CreateClient();

            var response = await client.GetAsync("/api/v1/Product/" + "1234");

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetProduct_NotFound()
        {
            var application = new WebApplication();
            var client = application.CreateClient();
            var guid = new Guid();

            var response = await client.GetAsync("/api/v1/Product/" + guid);

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_OK()
        {
            var application = new WebApplication();
            var client = application.CreateClient();
            var guid = new Guid("5E7BE964-BDBF-438D-AA79-FB324E1AE09E");

            var response = await client.DeleteAsync("/api/v1/Product/" + guid);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task DeleteProduct_InvalidGuid()
        {
            var application = new WebApplication();
            var client = application.CreateClient();
            var guid = new Guid();

            var response = await client.DeleteAsync("/api/v1/Product/" + guid);

            Assert.Equal(System.Net.HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_Ok()
        {
            var application = new WebApplication();
            var client = application.CreateClient();
            var guid = new Guid("0448C4E5-6256-4400-89FB-605A6820EC09");

            var req = new Product()
            {
                Id = guid,
                Name = "TestUpdate",
                Price = 777,
                Picture = null,
                Code = "665"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/v1/Product", content);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task UpdateProduct_InvalidData()
        {
            var application = new WebApplication();
            var client = application.CreateClient();
            var guid = new Guid("0448C4E5-6256-4400-89FB-605A6820EC09");

            var req = new Product()
            {
                Id = guid,
                Name = "TestUpdate",
                Code = "111",
                Price = 7778,
            };

            var json = System.Text.Json.JsonSerializer.Serialize(req);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync("/api/v1/Product/", content);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        }

    }

}
