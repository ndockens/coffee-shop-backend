using System.Collections.Generic;
using CoffeeShop.API.Models;

namespace CoffeeShop.API.Repositories
{
    public interface IProductsRepository
    {
        List<Product> Get();
        Product Get(int id);
        void Add(Product product);
        void Update(Product product);
        void Remove(int id);
    }
}