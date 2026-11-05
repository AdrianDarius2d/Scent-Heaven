using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ScentHeaven.Api.Models;

namespace ScentHeaven.Api.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Products.AnyAsync()) return;   // already seeded

        var categories = new[]
        {
            new Category { Name = "Women's perfumes", Slug = "women" },
            new Category { Name = "Men's perfumes",   Slug = "men" },
            new Category { Name = "Unisex fragrances", Slug = "unisex" },
            new Category { Name = "Gift sets",         Slug = "gift-sets" }
        };
        var brands = new[]
        {
            new Brand { Name = "Maison Aurelia" },
            new Brand { Name = "Noir & Co" },
            new Brand { Name = "Velvet Rose" },
            new Brand { Name = "Atelier Lune" },
            new Brand { Name = "Orient Gold" },
            new Brand { Name = "Citrus Garden" }
        };
        db.Categories.AddRange(categories);
        db.Brands.AddRange(brands);

        Category C(string slug) => categories.First(c => c.Slug == slug);
        Brand B(string name) => brands.First(b => b.Name == name);
        Product P(string name, string brand, string cat, int ml, decimal price, int stock, string desc) => new()
        {
            Name = name, Brand = B(brand), Category = C(cat), SizeMl = ml,
            Price = price, Stock = stock, Description = desc
        };

        db.Products.AddRange(
            // Women's
            P("Rose Velours",     "Velvet Rose",    "women", 50,  289m, 25, "Eau de parfum with damask rose, peony and soft musk."),
            P("Jasmin Nocturne",  "Atelier Lune",   "women", 100, 349m, 18, "Night-blooming jasmine, tuberose and a warm vanilla base."),
            P("Vanille Soleil",   "Maison Aurelia", "women", 50,  259m, 30, "Sweet vanilla, mandarin and coconut milk."),
            P("Pivoine Blanche",  "Velvet Rose",    "women", 30,  199m, 40, "Fresh white peony, lychee and cedar."),
            P("Ambre Douce",      "Orient Gold",    "women", 100, 379m, 12, "Amber, caramel and a hint of cinnamon."),
            P("Fleur d'Oranger",  "Citrus Garden",  "women", 50,  229m, 22, "Orange blossom, neroli and white tea."),
            // Men's
            P("Noir Absolu",      "Noir & Co",      "men",   100, 369m, 20, "Black pepper, leather and dark woods."),
            P("Cedre Sauvage",    "Noir & Co",      "men",   50,  289m, 27, "Cedarwood, juniper and smoked vetiver."),
            P("Vetiver Brume",    "Atelier Lune",   "men",   100, 339m, 15, "Vetiver, grapefruit and misty green notes."),
            P("Cuir Imperial",    "Orient Gold",    "men",   100, 419m, 8,  "Suede leather, saffron and incense."),
            P("Bois Fume",        "Maison Aurelia", "men",   50,  299m, 24, "Smoky birch, tobacco leaf and tonka bean."),
            P("Agrume Frais",     "Citrus Garden",  "men",   100, 249m, 35, "Bergamot, lemon zest and fresh mint."),
            // Unisex
            P("Santal Blanc",     "Atelier Lune",   "unisex", 50, 319m, 16, "Creamy sandalwood, iris and white musk."),
            P("Oud Mystere",      "Orient Gold",    "unisex", 50, 459m, 0,  "Rare oud, rose and spices (out of stock for demo)."),
            P("The Vert",         "Citrus Garden",  "unisex", 100, 239m, 28, "Green tea, ginger and light citrus."),
            P("Musc Pur",         "Maison Aurelia", "unisex", 30, 189m, 45, "Clean white musk and cotton flower."),
            // Gift sets
            P("Coffret Rose",     "Velvet Rose",    "gift-sets", 90,  399m, 10, "Three 30 ml rose fragrances in a gift box."),
            P("Coffret Noir",     "Noir & Co",      "gift-sets", 100, 449m, 9,  "100 ml Noir Absolu with a travel spray."),
            P("Coffret Decouverte","Atelier Lune",  "gift-sets", 120, 349m, 14, "Four 30 ml discovery sprays."),
            P("Coffret Agrumes",  "Citrus Garden",  "gift-sets", 90,  279m, 20, "Three citrus fragrances in a gift box.")
        );

        // Demo accounts. Passwords are for local development only.
        var hasher = new PasswordHasher<User>();
        var admin = new User { Email = "admin@scentheaven.test", FullName = "Store Admin", Role = "Admin" };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
        var customer = new User { Email = "customer@scentheaven.test", FullName = "Demo Customer", Role = "Customer" };
        customer.PasswordHash = hasher.HashPassword(customer, "Customer123!");
        db.Users.AddRange(admin, customer);

        await db.SaveChangesAsync();
    }
}
