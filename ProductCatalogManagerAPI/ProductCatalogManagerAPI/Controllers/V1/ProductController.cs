using AutoMapper;
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
        private readonly IProductService _service;
        private readonly IMapper _mapper;
        public ProductController(IProductService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
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
        public async Task<IActionResult> AddProduct(ProductDTO request)
        {
            var product = _mapper.Map<Product>(request);
            var respond = await _service.AddProduct(product);
            return CreatedAtAction(nameof(GetProduct), new { id = respond }, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<int> DeleteProduct(Guid id)
        {
            return await _service.DeleteProduct(id);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        public async Task UpdateProduct(ProductDTO request)
        {
            var product = _mapper.Map<Product>(request);
            var respond = await _service.UpdateProduct(product);
        }
    }
}
