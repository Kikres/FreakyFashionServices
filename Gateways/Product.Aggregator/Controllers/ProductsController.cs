using Microsoft.AspNetCore.Mvc;
using Product.Aggregator.Models;
using Product.Aggregator.Service;

namespace Product.Aggregator.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductsController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ProductFullDto>))]
    public async Task<ActionResult<IEnumerable<ProductFullDto>>> GetProducts() => Ok(await _productService.GetProducts());
}