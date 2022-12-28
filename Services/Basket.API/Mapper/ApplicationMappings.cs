using AutoMapper;
using Basket.API.Models;
using Basket.API.Models.Domain;
using Basket.API.Models.Dto;

namespace Basket.API.Mapper;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<Models.Domain.Basket, BasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();
    }
}