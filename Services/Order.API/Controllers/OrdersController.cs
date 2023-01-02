using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models;
using Order.API.Service;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly BasketService _basketService;

    public OrdersController(OrderService orderService, BasketService basketService)
    {
        _orderService = orderService;
        _basketService = basketService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderCreateResponseDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderCreateResponseDto>> CreateOrder(NewOrderDto newOrderDto)
    {
        if (!await _basketService.BasketExists(newOrderDto.Identifier)) return BadRequest("Basket empty");
        var productMinimalDto = _orderService.CreateOrderRequest(newOrderDto);
        return Created("", productMinimalDto);
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OrderDto>))]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetCustomerRelatedOrders(int customerId) => Ok(await _orderService.GetOrdersByCustomerId(customerId));
}