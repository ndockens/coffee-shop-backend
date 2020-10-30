using System.Collections.Generic;
using CoffeeShop.API.DTOs;

namespace CoffeeShop.API.Services
{
    public interface IProductsService
    {
        List<ProductDTO> Get();
        ProductDTO Get(int id);
        ProductDTO Get(string name);
        void Add(ProductDTO product);
        void Update(ProductDTO product);
        void Remove(int id);
    }
}