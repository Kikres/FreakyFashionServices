using Order.API.Models.Domain;
using Order.API.Models.Dto;

namespace Order.API.Client;

public class BasketCllient
{
    private readonly HttpClient _httpClient;

    public BasketCllient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BasketDto?> GetBasketByIdentifier(string identifier)
    {
        return await _httpClient.GetFromJsonAsync<BasketDto>($"/api/baskets/{identifier}");
    }

    public void DeleteBasketByIdentifier(string identifier)
    {
        _httpClient.DeleteAsync($"/api/baskets/{identifier}");
    }
}