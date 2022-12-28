using AutoMapper;
using Basket.API.Models.Dto;
using Basket.API.Repository;

namespace Basket.API.Service;

public class BasketService
{
    private readonly BasketRepository _basketRepository;
    private readonly IMapper _mapper;

    public BasketService(BasketRepository basketRepository, IMapper mapper)
    {
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<BasketDto> GetBasket(string identifier)
    {
        var basket = await _basketRepository.GetBasket(identifier);
        return _mapper.Map<BasketDto>(basket);
    }

    public async Task UpdateBasket(BasketDto basketDto)
    {
        var basket = _mapper.Map<Models.Domain.Basket>(basketDto);
        await _basketRepository.UpdateBasket(basket);
    }

    public async Task DeleteBasket(string identifier)
    {
        await _basketRepository.DeleteBasket(identifier);
    }
}