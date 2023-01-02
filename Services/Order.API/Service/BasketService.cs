using Order.Processor.Client;

namespace Order.API.Service;

public class BasketService
{
    private readonly BasketCllient _basketClient;

    public BasketService(BasketCllient basketClient)
    {
        _basketClient = basketClient;
    }

    public async Task<bool> BasketExists(string identifier)
    {
        return await _basketClient.GetBasketByIdentifier(identifier) != null;
    }
}