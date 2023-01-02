using Order.Processor.Models;
using System.Net.Http.Json;

namespace Order.Processor.Client;

public class BasketCllient
{
    private readonly HttpClient _httpClient;

    public BasketCllient()
    {
        //_httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:8002") };
        _httpClient = new HttpClient() { BaseAddress = new Uri("http://basket.api") };
    }

    public async Task<BasketDto?> GetBasketByIdentifier(string identifier)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<BasketDto>($"/api/baskets/{identifier}");
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void DeleteBasketByIdentifier(string identifier)
    {
        _httpClient.DeleteAsync($"/api/baskets/{identifier}");
    }
}