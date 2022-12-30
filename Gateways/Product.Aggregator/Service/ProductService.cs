using Product.Aggregator.Client;
using Product.Aggregator.Models;

namespace Product.Aggregator.Service;

public class ProductService
{
    private readonly CatalogClient _catalogClient;
    private readonly StockClient _stockClient;

    public ProductService(StockClient stockClient, CatalogClient catalogClient)
    {
        _stockClient = stockClient;
        _catalogClient = catalogClient;
    }

    public async Task<IEnumerable<ProductFullDto>?> GetProducts()
    {
        var productDtos = await _catalogClient.GetProducts();
        if (productDtos == null) return null;

        var stockDtos = await _stockClient.GetStockLevelsBySkus(productDtos.Select(o => o.Sku));
        if (stockDtos == null) return null;

        var ProductFullDtos = productDtos.Join(stockDtos, arg => arg.Sku, arg => arg.Sku,
        (first, second) => new ProductFullDto
        {
            Id = first.Id,
            Name = first.Name,
            Description = first.Description,
            ImageUrl = first.ImageUrl,
            Price = first.Price,
            Sku = first.Sku,
            UrlSlug = first.UrlSlug,
            Stock = second.Stock
        });

        return ProductFullDtos;
    }
}