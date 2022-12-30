using Microsoft.AspNetCore.Mvc;
using Stock.API.Data;
using Stock.API.Models;

namespace Stock.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StockLevelController : ControllerBase
{
    public readonly ApplicationContext context;

    public StockLevelController(ApplicationContext context)
    {
        this.context = context;
    }

    // PUT /api/stocklevel/ABC123
    [HttpPut("{sku}")]
    public IActionResult UpdateStockLevel(string sku, StockLevelDto newStockLevelDto)
    {
        if (newStockLevelDto.Sku != sku)
            return BadRequest("Sku does not match");

        var stockLevel = context.StockLevel.FirstOrDefault(x => x.Sku == newStockLevelDto.Sku);

        bool stockLevelExists = stockLevel != null;

        if (stockLevelExists)
        {
            stockLevel.Stock = newStockLevelDto.Stock;
        }
        else
        {
            stockLevel = new StockLevel(newStockLevelDto.Sku, newStockLevelDto.Stock);

            context.StockLevel.Add(stockLevel);
        }

        context.SaveChanges();

        return NoContent(); // 204 No Content
    }

    [HttpGet]
    public IEnumerable<StockLevelDto> GetStockLevels(string skus)
    {
        var skuList = skus.Split(',').ToList();

        var stockLevels = context.StockLevel.Where(x => skuList.Any(y => y == x.Sku));

        var result = stockLevels.Select(x => new StockLevelDto
        {
            Sku = x.Sku,
            Stock = x.Stock,
        });

        return result;
    }
}

public class StockLevelDto
{
    public string Sku { get; set; }
    public int Stock { get; set; }
}