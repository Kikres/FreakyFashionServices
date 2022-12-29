using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.API.Models.Dto;
using Order.API.Service;

namespace Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrdersController(OrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OrderMinimalDto))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderMinimalDto>> CreateOrder(NewOrderDto newOrderDto)
    {
        var productMinimalDto = await _orderService.CreateOrder(newOrderDto);
        if (productMinimalDto == null) return BadRequest("Basket not found");
        return Created("", productMinimalDto);
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Models.Domain.Order>))]
    public async Task<ActionResult<IEnumerable<Models.Domain.Order>>> GetCustomerRelatedOrders(int customerId) => Ok(await _orderService.GetOrdersByCustomerId(customerId));
}