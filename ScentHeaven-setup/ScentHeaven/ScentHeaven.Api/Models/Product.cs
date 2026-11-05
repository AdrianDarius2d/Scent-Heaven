namespace ScentHeaven.Api.Models;

public class Product
{
    public int Id { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; } = null!;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public int SizeMl { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string ImageUrl { get; set; } = "/img/placeholder.svg";
    public bool IsActive { get; set; } = true;   // soft delete
}
