using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.API.DTOs;
using CoffeeShop.API.Models;
using CoffeeShop.API.Repositories;

namespace CoffeeShop.API.Services
{
    public class ProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public List<ProductDTO> Get()
        {
            var products = _productsRepository.Get();

            return products.Select(x => new ProductDTO
            {
                Id = x.Id,
                Name = x.Name,
                DisplayName = x.DisplayName,
                Description = x.Description,
                CategoryId = x.CategoryId
            }).ToList();
        }

        public ProductDTO Get(int id)
        {
            var product = _productsRepository.Get(id);

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                DisplayName = product.DisplayName,
                Description = product.Description,
                CategoryId = product.CategoryId
            };
        }

        public void Add(ProductDTO product)
        {
            var productModel = new Product
            {
                Id = product.Id,
                Name = product.Name,
                DisplayName = product.DisplayName,
                Description = product.Description,
                CategoryId = product.CategoryId
            };

            _productsRepository.Add(productModel);
        }

        public void Update(ProductDTO product)
        {
            var productModel = new Product
            {
                Id = product.Id,
                Name = product.Name,
                DisplayName = product.DisplayName,
                Description = product.Description,
                CategoryId = product.CategoryId
            };

            _productsRepository.Update(productModel);
        }

        public void Remove(int id)
        {
            _productsRepository.Remove(id);
        }
    }
}