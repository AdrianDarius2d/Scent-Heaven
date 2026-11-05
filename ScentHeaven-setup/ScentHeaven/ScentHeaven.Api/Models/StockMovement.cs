namespace ScentHeaven.Api.Models;

public class StockMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int? OrderId { get; set; }
    public Order? Order { get; set; }

    public int Delta { get; set; }          // negative = sale, positive = restock
    public string Reason { get; set; } = "";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
