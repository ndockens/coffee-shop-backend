using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeShop.API.Models;

namespace CoffeeShop.API.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly CoffeeShopContext _context;

        public ProductsRepository(CoffeeShopContext context)
        {
            _context = context;
        }

        public List<Product> Get()
        {
            return _context.Products.ToList(); ;
        }

        public Product Get(int id)
        {
            return _context.Products.FirstOrDefault(x => x.Id == id);
        }

        public Product Get(string name)
        {
            return _context.Products.FirstOrDefault(x => x.Name == name);
        }

        public void Add(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            var existingProduct = _context.Products.FirstOrDefault(x => x.Id == product.Id);

            if (existingProduct == null)
                return;

            existingProduct.Name = product.Name;
            existingProduct.DisplayName = product.DisplayName;
            existingProduct.Description = product.Description;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Category = product.Category;

            _context.SaveChanges();
        }

        public void Remove(int id)
        {
            var existingProduct = _context.Products.FirstOrDefault(x => x.Id == id);

            if (existingProduct == null)
                return;

            _context.Products.Remove(existingProduct);
            _context.SaveChanges();
        }
    }
}