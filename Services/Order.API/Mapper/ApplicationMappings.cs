using AutoMapper;
using Order.API.Models;

namespace Order.API.Mapper;

public class ApplicationMappings : Profile
{
    public ApplicationMappings()
    {
        CreateMap<NewOrderDto, NewOrderEventRequestDto>().ReverseMap();
        CreateMap<Data.Models.Order, OrderDto>().ReverseMap();
        CreateMap<Data.Models.OrderLine, OrderLineDto>().ReverseMap();
    }
}