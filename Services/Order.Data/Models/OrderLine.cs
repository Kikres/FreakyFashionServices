namespace Order.Data.Models;

public class OrderLine
{
    public int Id { get; set; }
    public string Sku { get; set; }
    public int Quantity { get; set; }
}