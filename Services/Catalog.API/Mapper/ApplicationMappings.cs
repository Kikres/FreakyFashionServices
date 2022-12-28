using AutoMapper;
using Catalog.API.Models.Domain;
using Catalog.API.Models.Dto;

namespace Catalog.API.Mapper;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Product, NewProductDto>().ReverseMap();
    }
}