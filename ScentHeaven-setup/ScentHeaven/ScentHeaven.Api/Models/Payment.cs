namespace ScentHeaven.Api.Models;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public Order Order { get; set; } = null!;

    public string StripeSessionId { get; set; } = "";
    public string? StripeEventId { get; set; }   // unique: makes webhooks idempotent
    public string Status { get; set; } = "";
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
