using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CoffeeShop.API.DTOs;
using CoffeeShop.API.Services;

namespace CoffeeShop.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet("")]
        public ActionResult<IEnumerable<ProductDTO>> Get()
        {
            return _productsService.Get();
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> Get(int id)
        {
            return _productsService.Get(id);
        }

        [HttpPost("")]
        public ActionResult Post(ProductDTO product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Name cannot be null or empty");

            if (product.CategoryId == 0)
                return BadRequest("Category ID cannot be null or zero");

            _productsService.Add(product);
            var createdProduct = _productsService.Get(product.Name);

            return Created($"products/{createdProduct.Id}", createdProduct);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, ProductDTO product)
        {
            if (_productsService.Get(id) == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Name cannot be null or empty");

            if (product.CategoryId == 0)
                return BadRequest("Category ID cannot be null or zero");

            if (product.Id == 0)
                product.Id = id;

            _productsService.Update(product);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_productsService.Get(id) == null)
                return NotFound();

            _productsService.Remove(id);

            return NoContent();
        }
    }
}