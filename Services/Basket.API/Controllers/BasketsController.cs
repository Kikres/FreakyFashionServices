using Basket.API.Models.Dto;
using Basket.API.Service;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketsController : ControllerBase
{
    private readonly BasketService _basketService;

    public BasketsController(BasketService basketService)
    {
        _basketService = basketService;
    }

    [HttpPut("{identifier}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateBasket(string identifier, BasketDto basketDto)
    {
        if (identifier != basketDto.Identifier) return BadRequest("Identifier mismatch");
        await _basketService.UpdateBasket(basketDto);
        return Created("", null);
    }

    [HttpGet("{identifier}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasketDto))]
    public async Task<ActionResult<BasketDto>> GetProducts(string identifier) => Ok(await _basketService.GetBasket(identifier));

    [HttpDelete("{identifier}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteBasket(string identifier)
    {
        await _basketService.DeleteBasket(identifier);
        return NoContent();
    }
}