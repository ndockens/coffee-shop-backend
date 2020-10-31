using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CoffeeShop.API.Repositories;
using CoffeeShop.API.Models;

namespace CoffeeShop.Tests.Repositories
{
    public class ProductsRepositoryTests
    {
        private ProductsRepository _repository;

        public ProductsRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CoffeeShopContext>()
                .UseInMemoryDatabase("CoffeeShop")
                .Options;

            _repository = new ProductsRepository(new CoffeeShopContext(options));
        }

        [Fact]
        public void Get_NoInput_ShouldReturn5Products()
        {
            var result = _repository.Get();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void Get_InputIs1_ShouldReturnProductWithIdEqualTo1()
        {
            var result = _repository.Get(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Get_InputIsLatte_ShouldReturnProductWithNameEqualToLatte()
        {
            var result = _repository.Get("Latte");

            Assert.Equal("Latte", result.Name);
        }

        [Fact]
        public void Add_InputIsNewProduct_ShouldAddProductToDatabase()
        {
            var input = new Product
            {
                Id = 6,
                Name = "Some New Product"
            };

            _repository.Add(input);
            var result = _repository.Get(6);

            Assert.Same(input, result);
        }

        [Fact]
        public void Update_InputIsExistingProduct_ShouldUpdateProductProperties()
        {
            var input = new Product
            {
                Id = 1,
                Name = "Drip Coffee (Updated)",
                DisplayName = null,
                Description = null,
                CategoryId = 2
            };

            _repository.Update(input);
            var result = _repository.Get(1);

            // Todo: Refactor this code so there's only one assert
            Assert.Equal(input.Name, result.Name);
            Assert.Equal(input.DisplayName, result.DisplayName);
            Assert.Equal(input.Description, result.Description);
            Assert.Equal(input.CategoryId, result.CategoryId);
            Assert.Equal(input.Category, result.Category);
        }

        [Fact]
        public void Remove_InputIsExistingProduct_ShouldRemoveProductFromDatabase()
        {
            _repository.Remove(1);
            var result = _repository.Get(1);

            Assert.Null(result);
        }
    }
}