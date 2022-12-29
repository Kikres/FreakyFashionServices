namespace Order.API.Models.Domain;

public class OrderLine
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
}