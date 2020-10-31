using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CoffeeShop.API.DTOs;
using CoffeeShop.API.Models;
using CoffeeShop.API.Repositories;

namespace CoffeeShop.API.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductsService(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }

        public List<ProductDTO> Get()
        {
            var products = _productsRepository.Get();

            return _mapper.Map<List<ProductDTO>>(products);
        }

        public ProductDTO Get(int id)
        {
            var product = _productsRepository.Get(id);

            if (product == null)
                return null;

            return _mapper.Map<ProductDTO>(product);
        }

        public ProductDTO Get(string name)
        {
            var product = _productsRepository.Get(name);

            if (product == null)
                return null;

            return _mapper.Map<ProductDTO>(product);
        }

        public void Add(ProductDTO product)
        {
            product.Id = _productsRepository.Get().Max(x => x.Id) + 1;
            _productsRepository.Add(_mapper.Map<Product>(product));
        }


        public void Update(ProductDTO product)
        {
            _productsRepository.Update(_mapper.Map<Product>(product));
        }

        public void Remove(int id)
        {
            _productsRepository.Remove(id);
        }
    }
}