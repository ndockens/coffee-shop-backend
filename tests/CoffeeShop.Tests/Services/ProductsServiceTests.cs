using System.Collections.Generic;
using System.Linq;
using Moq;
using Xunit;
using CoffeeShop.API.DTOs;
using CoffeeShop.API.Models;
using CoffeeShop.API.Repositories;
using CoffeeShop.API.Services;

namespace CoffeeShop.Tests.Services
{
    public class ProductsServiceTests
    {
        private ProductsService _service;
        private Mock<IProductsRepository> _productsRepositoryMock;
        private List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Drip Coffee", CategoryId = 1 },
            new Product { Id = 2, Name = "Latte", CategoryId = 2 },
            new Product { Id = 3, Name = "Cappuccino", CategoryId = 2 }
        };

        public ProductsServiceTests()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();
            _productsRepositoryMock.Setup(x => x.Get())
                .Returns(_products);
            _productsRepositoryMock.Setup(x => x.Get(1))
                .Returns(_products[0]);
            _productsRepositoryMock.Setup(x => x.Get(999))
                .Returns(null as Product);
            _productsRepositoryMock.Setup(x => x.Get("Latte"))
                .Returns(_products[1]);
            _productsRepositoryMock.Setup(x => x.Get("blah"))
                .Returns(null as Product);
            _productsRepositoryMock.Setup(x => x.Add(It.IsAny<Product>()));
            _productsRepositoryMock.Setup(x => x.Update(It.IsAny<Product>()));
            _productsRepositoryMock.Setup(x => x.Remove(It.IsAny<int>()));

            _service = new ProductsService(_productsRepositoryMock.Object);
        }

        [Fact]
        public void Get_NoInput_ShouldCallRepositoryGetMethodOneTime()
        {
            _service.Get();

            _productsRepositoryMock.Verify(x => x.Get(), Times.Once);
        }

        [Fact]
        public void Get_NoInput_ShouldReturnAllProducts()
        {
            var result = _service.Get();

            Assert.Equal(_products.Count, result.Count);
        }

        [Fact]
        public void Get_InputIs1_ShouldCallRepositoryGetMethodOneTimeWithInputEqualTo1()
        {
            _service.Get(1);

            _productsRepositoryMock.Verify(x => x.Get(1), Times.Once);
        }

        [Fact]
        public void Get_InputIs1_ShouldReturnProductWithIdEqualTo1()
        {
            var result = _service.Get(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Get_InputIs999_ShouldReturnNull()
        {
            var result = _service.Get(999);

            Assert.Equal(null, result);
        }

        [Fact]
        public void Get_InputIsLatte_ShouldCallRepositoryGetMethodOneTimeWithInputEqualToLatte()
        {
            _service.Get("Latte");

            _productsRepositoryMock.Verify(x => x.Get("Latte"), Times.Once);
        }

        [Fact]
        public void Get_InputIsLatte_ShouldReturnProductWithNameEqualToLatte()
        {
            var result = _service.Get("Latte");

            Assert.Equal("Latte", result.Name);
        }

        [Fact]
        public void Get_InputIsBlah_ShouldReturnNull()
        {
            var result = _service.Get("blah");

            Assert.Equal(null, result);
        }

        [Fact]
        public void Add_InputIsProduct_ShouldCallRepositoryAddMethodOneTimeWithInputEqualToProduct()
        {
            var input = new ProductDTO { Name = "Some New Product" };

            _service.Add(input);

            // Product ID should be auto-populated based on max ID currently in the database
            var maxProductId = _products.Max(x => x.Id);

            _productsRepositoryMock.Verify(x =>
                x.Add(It.Is<Product>(y => y.Id == maxProductId + 1 && y.Name == input.Name))
                , Times.Once);
        }

        [Fact]
        public void Update_InputIsProduct_ShouldCallRepositoryUpdateMethodOneTimeWithInputEqualToProduct()
        {
            var input = new ProductDTO
            {
                Id = 1,
                Name = "Drip Coffee (Updated)"
            };

            _service.Update(input);

            _productsRepositoryMock.Verify(x =>
                x.Update(It.Is<Product>(y => y.Id == input.Id && y.Name == input.Name))
                , Times.Once);
        }

        [Fact]
        public void Remove_InputIsProduct_ShouldCallRepositoryRemoveMethodOneTimeWithInputEqualTo1()
        {
            _service.Remove(1);

            _productsRepositoryMock.Verify(x => x.Remove(1), Times.Once);
        }
    }
}