namespace Catalog.API.Models.Dto;

public class NewProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public double Price { get; set; }
    public string Sku { get; set; }
}