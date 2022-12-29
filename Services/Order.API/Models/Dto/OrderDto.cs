namespace Order.API.Models.Dto;

public class OrderDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public ICollection<OrderLineDto> Products { get; set; }
}