using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class ProductContextSeed
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if (products == null) return;
            context.products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
