﻿using Order.Processor.Models;
using System.Net.Http.Json;

namespace Order.Processor.Client;

public class BasketCllient
{
    private readonly HttpClient _httpClient;

    public BasketCllient(HttpClient httpClient)
    {
        _httpClient = httpClient;
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
}