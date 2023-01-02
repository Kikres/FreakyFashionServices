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

        var ProductFullDtos = from p in productDtos
                              join s in stockDtos on p.Sku equals s.Sku into joined
                              from s in joined.DefaultIfEmpty()
                              select new ProductFullDto
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Description = p.Description,
                                  ImageUrl = p.ImageUrl,
                                  Price = p.Price,
                                  Sku = p.Sku,
                                  UrlSlug = p.UrlSlug,
                                  Stock = s?.Stock ?? 0
                              };

        return ProductFullDtos;
    }
}