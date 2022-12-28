namespace Basket.API.Models.Dto;

public class BasketDto
{
    public string Identifier { get; set; }
    public ICollection<BasketItemDto> Items { get; set; }
}