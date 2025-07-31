
using AutoMapper;
using SuperMarketAPI.Models.DTOs;
using SuperMarketAPI.Models;
using SuperMarketAPI.DTOs;

namespace SuperMarketAPI;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product mappings
        CreateMap<Models.DTOs.ProductCreateDto, Product>();
        //CreateMap<Product, ProductCreateDto>();
        CreateMap<Product, Models.DTOs.ProductReadDto>()
            .ForMember(dest => dest.CategoryName,
                     opt => opt.MapFrom(src => src.Category.Name));
        CreateMap<Models.DTOs.ProductUpdateDto, Product>().ReverseMap();

        // Category mappings
        CreateMap<Category, CategoryReadDto>()
            .ForMember(dest => dest.Products,
                opt => opt.MapFrom(src => src.Products));
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<Product, CategoryProductDto>();
        CreateMap<CategoryUpdateDto, Category>();
    }
}
