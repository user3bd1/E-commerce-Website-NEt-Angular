using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
   public static async Task SeedAsync(StoreContext context)
    {
        if(!context.Products.Any())
        {
            var ProductData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(ProductData);
            if (products is null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}
