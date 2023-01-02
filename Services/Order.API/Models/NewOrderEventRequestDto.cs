namespace Order.API.Models;

public class NewOrderEventRequestDto
{
    public Guid Id { get; set; }
    public string Identifier { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}