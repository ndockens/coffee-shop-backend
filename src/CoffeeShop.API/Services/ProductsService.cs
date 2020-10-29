using System;
using System.Collections.Generic;
using CoffeeShop.API.DTOs;
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
            throw new NotImplementedException();
        }

        public ProductDTO Get(int id)
        {
            throw new NotImplementedException();
        }

        public void Add(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductDTO product)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}