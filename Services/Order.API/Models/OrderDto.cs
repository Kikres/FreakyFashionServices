namespace Order.API.Models;

public class OrderDto
{
    public Guid Id { get; set; }
    public int CustomerId { get; set; }
    public ICollection<OrderLineDto> Products { get; set; }
}