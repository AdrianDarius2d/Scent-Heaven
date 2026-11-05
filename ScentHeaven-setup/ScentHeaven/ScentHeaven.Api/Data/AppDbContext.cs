using Microsoft.EntityFrameworkCore;

namespace ScentHeaven.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Step 2 (database): add DbSet<User>, DbSet<Product>, ... from the ER diagram here.
}
