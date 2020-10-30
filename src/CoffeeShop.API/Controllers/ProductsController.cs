using System;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Get()
        {
            throw new NotImplementedException();
        }
    }
}