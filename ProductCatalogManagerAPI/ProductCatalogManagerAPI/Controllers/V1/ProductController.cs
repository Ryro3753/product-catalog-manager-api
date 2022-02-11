using Microsoft.AspNetCore.Mvc;
using ProductCatalogManagerAPI.Models;
using ProductCatalogManagerAPI.Services;
using System.Collections.Generic;
using System.Net;

namespace ProductCatalogManagerAPI.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class ProductController : Controller
    {
        readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _service.GetProducts();
        }

        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<Product> GetProduct(Guid id)
        {
            return await _service.GetProduct(id);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task<IActionResult> AddProduct(Product request)
        {
            var respond = await _service.AddProduct(request);
            return CreatedAtAction(nameof(GetProduct), new { id = respond }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task DeleteProduct(Guid id)
        {
            await _service.DeleteProduct(id);
        }
    }
}
