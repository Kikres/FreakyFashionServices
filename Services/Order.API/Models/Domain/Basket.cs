namespace Order.API.Models.Domain;

public class Basket
{
    public string Identifier { get; set; }
    public ICollection<BasketItem> Items { get; set; }
}