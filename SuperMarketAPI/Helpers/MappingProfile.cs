using AutoMapper;
using SuperMarketAPI.Models;
using SuperMarketAPI.Models.DTOs;

namespace SuperMarketAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<CategoryCreateDTO, Category>();
            CreateMap<CategoryUpdateDTO, Category>();

        }
    }
}
