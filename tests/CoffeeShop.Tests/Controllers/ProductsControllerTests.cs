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
            _serviceMock.Setup(x => x.Get(1)).Returns(new ProductDTO());
            _serviceMock.Setup(x => x.Get(999)).Returns(null as ProductDTO);
            _serviceMock.Setup(x => x.Get("Some New Product")).Returns(new ProductDTO { Id = 999, Name = "Some New Product", CategoryId = 1 });

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
            var result = _controller.Get();

            Assert.Equal(new List<ProductDTO>(), result.Value);
        }

        [Fact]
        public void Get_InputIs1_ShouldCallServiceGetMethodWithInputEqualTo1()
        {
            _controller.Get(1);

            _serviceMock.Verify(x => x.Get(1), Times.Once);
        }

        [Fact]
        public void Get_InputIs1_ShouldReturnSingleProduct()
        {
            var result = _controller.Get(1);

            Assert.Equal(typeof(ProductDTO), result.Value.GetType());
        }

        [Fact]
        public void Post_InputIsValidProduct_ShouldCallServiceAddMethodWithInputEqualToProduct()
        {
            var product = new ProductDTO { Name = "Some New Product", CategoryId = 1 };

            _controller.Post(product);

            _serviceMock.Verify(x => x.Add(product), Times.Once);
        }

        [Fact]
        public void Post_InputIsValidProduct_ShouldReturnCorrectUrl()
        {
            var product = new ProductDTO { Name = "Some New Product", CategoryId = 1 };

            var result = _controller.Post(product) as CreatedResult;

            Assert.Equal($"products/999", result.Location);
        }

        [Fact]
        public void Post_InputIsValidProduct_ShouldReturnCreatedObject()
        {
            var product = new ProductDTO { Name = "Some New Product", CategoryId = 1 };

            var result = _controller.Post(product) as CreatedResult;

            Assert.Equal(product.Name, ((ProductDTO)result.Value).Name);
        }

        [Fact]
        public void Post_InputIsProductWithNoCategoryId_ShouldReturnBadRequest()
        {
            var product = new ProductDTO { Name = "Some New Product" };

            var result = _controller.Post(product);

            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public void Post_InputIsProductWithNoName_ShouldReturnBadRequest()
        {
            var product = new ProductDTO { CategoryId = 1 };

            var result = _controller.Post(product);

            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public void Put_InputsAre1AndValidProduct_ShouldCallServiceUpdateMethodWithInputEqualToProduct()
        {
            var product = new ProductDTO { Id = 1, Name = "Drip Coffee (Updated)", CategoryId = 1 };

            _controller.Put(product.Id, product);

            _serviceMock.Verify(x => x.Update(product), Times.Once);
        }

        [Fact]
        public void Put_InputsAre1AndValidProduct_ShouldReturnNoContent()
        {
            var product = new ProductDTO { Id = 1, Name = "Drip Coffee (Updated)", CategoryId = 1 };

            var result = _controller.Put(product.Id, product);

            Assert.Equal(typeof(NoContentResult), result.GetType());
        }

        [Fact]
        public void Put_InputsAre999AndValidProduct_ShouldReturnNotFound()
        {
            var product = new ProductDTO { Id = 999, Name = "Non-Existent Product", CategoryId = 1 };

            var result = _controller.Put(product.Id, product);

            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }

        [Fact]
        public void Put_InputIsProductWithNoCategoryId_ShouldReturnBadRequest()
        {
            var product = new ProductDTO { Id = 1, Name = "Drip Coffee (Updated)" };

            var result = _controller.Put(product.Id, product);

            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public void Put_InputIsProductWithNoName_ShouldReturnBadRequest()
        {
            var product = new ProductDTO { Id = 1, CategoryId = 1 };

            var result = _controller.Put(product.Id, product);

            Assert.Equal(typeof(BadRequestObjectResult), result.GetType());
        }

        [Fact]
        public void Delete_InputIs1_ShouldCallServiceRemoveMethodWithInputEqualTo1()
        {
            _controller.Delete(1);

            _serviceMock.Verify(x => x.Remove(1), Times.Once);
        }

        [Fact]
        public void Delete_InputIs1_ShouldReturnNoContent()
        {
            var result = _controller.Delete(1);

            Assert.Equal(typeof(NoContentResult), result.GetType());
        }

        [Fact]
        public void Delete_InputIs999_ShouldReturnNotFound()
        {
            var result = _controller.Delete(999);

            Assert.Equal(typeof(NotFoundResult), result.GetType());
        }
    }
}