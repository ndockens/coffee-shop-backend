using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CoffeeShop.API.Controllers;
using CoffeeShop.API.DTOs;
using CoffeeShop.API.Services;

namespace CoffeeShop.Tests.Controllers
{
    public class ProductsControllerTests
    {
        private ProductsController _controller;
        private Mock<IProductsService> _serviceMock;

        public ProductsControllerTests()
        {
            _serviceMock = new Mock<IProductsService>();
            _serviceMock.Setup(x => x.Get()).Returns(new List<ProductDTO>());

            _controller = new ProductsController(_serviceMock.Object);
        }

        [Fact]
        public void Get_NoInput_ShouldCallServiceGetMethodWithNoInputs()
        {
            _controller.Get();

            _serviceMock.Verify(x => x.Get(), Times.Once);
        }

        [Fact]
        public void Get_NoInput_ShouldReturnListOfProducts()
        {
            var result = _controller.Get() as ActionResult<IEnumerable<ProductDTO>>;

            Assert.Equal(new List<ProductDTO>(), result.Value);
        }
    }
}