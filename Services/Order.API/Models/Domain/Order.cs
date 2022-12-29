namespace Order.API.Models.Domain;

public class Order
{
    public Order()
    {
        CreatedAt = DateTime.Now;
    }

    public int OrderId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public IEnumerable<OrderLine> Products { get; set; }
}