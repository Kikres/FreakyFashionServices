using Basket.API.Models.Domain;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repository;

public class BasketRepository
{
    private readonly IDistributedCache _redisCache;

    public BasketRepository(IDistributedCache redisCache)
    {
        _redisCache = redisCache;
    }

    public async Task<Models.Domain.Basket?> GetBasket(string identifier)
    {
        var basket = await _redisCache.GetStringAsync(identifier);
        if (String.IsNullOrEmpty(basket)) return null;

        return JsonConvert.DeserializeObject<Models.Domain.Basket>(basket);
    }

    public async Task<Models.Domain.Basket?> UpdateBasket(Models.Domain.Basket basket)
    {
        await _redisCache.SetStringAsync(basket.Identifier, JsonConvert.SerializeObject(basket));
        return await GetBasket(basket.Identifier);
    }

    public async Task DeleteBasket(string identifier)
    {
        await _redisCache.RemoveAsync(identifier);
    }
}