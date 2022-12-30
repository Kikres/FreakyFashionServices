using Microsoft.AspNetCore.WebUtilities;
using Product.Aggregator.Models;

namespace Product.Aggregator.Client;

public class StockClient
{
    private readonly HttpClient _httpClient;

    public StockClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<StockLevelDto>?> GetStockLevelsBySkus(IEnumerable<string> skus)
    {
        if (!skus.Any()) return null;

        var query = new Dictionary<string, string>
        {
            { "skus", String.Join(",", skus) }
        };

        var uri = QueryHelpers.AddQueryString("/api/stocklevel", query);

        return await _httpClient.GetFromJsonAsync<IEnumerable<StockLevelDto>>(uri);
    }
}