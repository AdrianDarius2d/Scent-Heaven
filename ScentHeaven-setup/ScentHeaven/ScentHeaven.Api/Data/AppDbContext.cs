using Microsoft.EntityFrameworkCore;
using ScentHeaven.Api.Models;

namespace ScentHeaven.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<StockMovement> StockMovements => Set<StockMovement>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<User>(e =>
        {
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.Email).HasMaxLength(256);
        });

        b.Entity<Category>().HasIndex(x => x.Slug).IsUnique();

        // SQLite stores decimal as text, which breaks ORDER BY / comparisons on price.
        // Converting to double keeps sorting and price filters working in SQL.
        b.Entity<Product>().Property(x => x.Price).HasConversion<double>();
        b.Entity<Order>().Property(x => x.Total).HasConversion<double>();
        b.Entity<OrderItem>().Property(x => x.UnitPrice).HasConversion<double>();
        b.Entity<Payment>().Property(x => x.Amount).HasConversion<double>();

        b.Entity<Order>().Property(x => x.Status).HasConversion<string>();

        b.Entity<CartItem>().HasIndex(x => new { x.UserId, x.ProductId }).IsUnique();
        b.Entity<Payment>().HasIndex(x => x.StripeEventId).IsUnique();   // NULLs allowed multiple times

        // Products are never hard-deleted, so referencing rows must not cascade.
        b.Entity<Product>().HasOne(x => x.Brand).WithMany(x => x.Products).OnDelete(DeleteBehavior.Restrict);
        b.Entity<Product>().HasOne(x => x.Category).WithMany(x => x.Products).OnDelete(DeleteBehavior.Restrict);
        b.Entity<OrderItem>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.Restrict);
        b.Entity<CartItem>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.Restrict);
        b.Entity<StockMovement>().HasOne(x => x.Product).WithMany().OnDelete(DeleteBehavior.Restrict);
        b.Entity<StockMovement>().HasOne(x => x.Order).WithMany().OnDelete(DeleteBehavior.SetNull);
        b.Entity<Order>().HasOne(x => x.User).WithMany(x => x.Orders).OnDelete(DeleteBehavior.Restrict);
    }
}
