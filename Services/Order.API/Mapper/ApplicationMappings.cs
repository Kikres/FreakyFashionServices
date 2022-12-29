using AutoMapper;
using Order.API.Models.Dto;
using Order.API.Models.Domain;

namespace Order.API.Mapper;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<Models.Domain.Order, OrderMinimalDto>().ReverseMap();
        CreateMap<Models.Domain.Order, OrderDto>().ReverseMap();
        CreateMap<Customer, NewOrderDto>().ReverseMap();
        CreateMap<OrderLine, OrderLineDto>().ReverseMap();
        CreateMap<Basket, BasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        CreateMap<BasketItem, OrderLine>().ReverseMap();
    }
}