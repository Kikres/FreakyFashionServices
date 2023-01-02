using System.ComponentModel.DataAnnotations;

namespace Order.Data.Models;

public class Order
{
    public Order()
    {
        CreatedAt = DateTime.Now;
    }

    [Key]
    public Guid Id { get; set; }

    public DateTime CreatedAt { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public ICollection<OrderLine> Products { get; set; }
}