using AutoMapper;
using Newtonsoft.Json;
using Order.API.Client;
using Order.API.Models;
using Order.Data.Repository;

namespace Order.API.Service;

public class OrderService
{
    private readonly EventBus _eventBus;
    private readonly IMapper _mapper;
    private readonly OrderRepositroy _orderRepositroy;

    public OrderService(IMapper mapper, OrderRepositroy orderRepositroy, EventBus eventBus)
    {
        _mapper = mapper;
        _orderRepositroy = orderRepositroy;
        _eventBus = eventBus;
    }

    public OrderCreateResponseDto CreateOrderRequest(NewOrderDto newOrderDto)
    {
        var newOrderRequestDto = _mapper.Map<NewOrderEventRequestDto>(newOrderDto);
        var orderGuid = Guid.NewGuid();
        newOrderRequestDto.Id = orderGuid;

        var payload = JsonConvert.SerializeObject(newOrderRequestDto);
        _eventBus.SendToBus("createorder", payload);

        return new OrderCreateResponseDto() { Id = orderGuid };
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerId(int customerId) => _mapper.Map<IEnumerable<OrderDto>>(await _orderRepositroy.GetOrdersByCustomerId(customerId));
}

#region
//using AutoMapper;
//using Order.API.Client;
//using Order.API.Models.Domain;
//using Order.API.Models.Dto;
//using Order.API.Repository;

//namespace Order.API.Service;

//public class OrderService
//{
//    private readonly OrderRepositroy _orderRepositroy;
//    private readonly CustomerRepositroy _customerRepositroy;
//    private readonly BasketCllient _basketCllient;
//    private readonly IMapper _mapper;

//    public OrderService(OrderRepositroy orderRepositroy, BasketCllient basketCllient, IMapper mapper, CustomerRepositroy customerRepositroy)
//    {
//        _orderRepositroy = orderRepositroy;
//        _basketCllient = basketCllient;
//        _mapper = mapper;
//        _customerRepositroy = customerRepositroy;
//    }

//    public async Task<OrderMinimalDto?> CreateOrder(NewOrderDto newOrderDto)
//    {
//        var basket = _mapper.Map<Basket>(await _basketCllient.GetBasketByIdentifier(newOrderDto.Identifier));
//        if (basket == null) return null;

//        var order = new Models.Domain.Order();

//        var existingCustomer = await _customerRepositroy.GetCustomerByEmail(newOrderDto.Email);
//        order.Customer = existingCustomer != null ? existingCustomer : _mapper.Map<Customer>(newOrderDto);
//        order.Products = _mapper.Map<IEnumerable<OrderLine>>(basket.Items);

//        var toReturn = _mapper.Map<OrderMinimalDto>(await _orderRepositroy.CreateOrder(order));

//        _basketCllient.DeleteBasketByIdentifier(newOrderDto.Identifier);
//        return toReturn;
//    }

//    public async Task<IEnumerable<OrderDto>> GetOrdersByCustomerId(int customerId) => _mapper.Map<IEnumerable<OrderDto>>(await _orderRepositroy.GetOrdersByCustomerId(customerId));
//}
#endregion