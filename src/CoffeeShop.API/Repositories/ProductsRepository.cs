using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.API.Models;

namespace CoffeeShop.API.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Drip Coffee", CategoryId = 1 },
            new Product { Id = 2, Name = "Latte", CategoryId = 2 },
            new Product { Id = 3, Name = "Cappuccino", CategoryId = 2 },
            new Product { Id = 4, Name = "Mocha", CategoryId = 2 },
            new Product { Id = 5, Name = "Smoothie", CategoryId = 3 }
        };

        public List<Product> Get()
        {
            return _products;
        }

        public Product Get(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        public Product Get(string name)
        {
            return _products.FirstOrDefault(x => x.Name == name);
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Update(Product product)
        {
            var existingProduct = _products.FirstOrDefault(x => x.Id == product.Id);

            if (existingProduct == null)
                return;

            existingProduct.Name = product.Name;
            existingProduct.DisplayName = product.DisplayName;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Category = product.Category;
        }

        public void Remove(int id)
        {
            var existingProduct = _products.FirstOrDefault(x => x.Id == id);

            if (existingProduct == null)
                return;

            _products.Remove(existingProduct);
        }
    }
}