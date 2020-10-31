using AutoMapper;
using CoffeeShop.API.DTOs;
using CoffeeShop.API.Models;

namespace CoffeeShop.API.MapperProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}