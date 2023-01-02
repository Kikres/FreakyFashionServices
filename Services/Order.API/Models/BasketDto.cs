namespace Order.Processor.Models;

public class BasketDto
{
    public string Identifier { get; set; }
    public ICollection<BasketItemDto> Items { get; set; }
}