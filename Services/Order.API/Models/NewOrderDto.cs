namespace Order.API.Models;

public class NewOrderDto
{
    public string Identifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}