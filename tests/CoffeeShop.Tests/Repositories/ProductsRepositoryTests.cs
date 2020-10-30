using System;
using System.Collections.Generic;
using Xunit;
using CoffeeShop.API.Repositories;
using CoffeeShop.API.Models;

namespace CoffeeShop.Tests.Repositories
{
    public class ProductsRepositoryTests
    {
        [Fact]
        public void Get_NoInput_ShouldReturn5Products()
        {
            var repo = new ProductsRepository();

            var result = repo.Get();

            Assert.Equal(5, result.Count);
        }

        [Fact]
        public void Get_InputIs1_ShouldReturnProductWithIdEqualTo1()
        {
            var repo = new ProductsRepository();

            var result = repo.Get(1);

            Assert.Equal(1, result.Id);
        }

        [Fact]
        public void Get_InputIsLatte_ShouldReturnProductWithNameEqualToLatte()
        {
            var repo = new ProductsRepository();

            var result = repo.Get("Latte");

            Assert.Equal("Latte", result.Name);
        }

        [Fact]
        public void Add_InputIsNewProduct_ShouldAddProductToDatabase()
        {
            var repo = new ProductsRepository();
            var input = new Product
            {
                Id = 6,
                Name = "Some New Product"
            };

            repo.Add(input);
            var result = repo.Get(6);

            Assert.Same(input, result);
        }

        [Fact]
        public void Update_InputIsExistingProduct_ShouldUpdateProductProperties()
        {
            var repo = new ProductsRepository();
            var input = new Product
            {
                Id = 1,
                Name = "Drip Coffee (Updated)",
                DisplayName = null,
                Description = null,
                CategoryId = 2
            };

            repo.Update(input);
            var result = repo.Get(1);

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
            var repo = new ProductsRepository();

            repo.Remove(1);
            var result = repo.Get(1);

            Assert.Null(result);
        }
    }
}