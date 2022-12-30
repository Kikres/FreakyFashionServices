using Product.Aggregator.Models;

namespace Product.Aggregator.Client;

public class CatalogClient
{
    private readonly HttpClient _httpClient;

    public CatalogClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductDto>?> GetProducts()
    {
        var response = await _httpClient.GetFromJsonAsync<IEnumerable<ProductDto>>($"/api/products");
        Console.WriteLine("catalog success");
        return response;
    }
}