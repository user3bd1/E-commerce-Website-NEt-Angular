using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{

    
    public void AddProdcut(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProdcut(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandAsync()
    {
        return await context.Products.Select(x=>x.Brand).Distinct().ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var foundproduct = await context.Products.FindAsync(id);
        if (foundproduct is null) throw new KeyNotFoundException($"Product with ID {id} was not found.") ;

        return foundproduct;
        
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types,string? sort)
    {
        var query = context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(brands)) query = query.Where(x=> x.Brand==brands);
        if (!string.IsNullOrWhiteSpace(types)) query = query.Where(x=> x.Type==types);

        query = sort switch
        {
            "priceAsc"=>query.OrderBy(x=>x.Price),
            "priceDesc"=>query.OrderByDescending(x=>x.Price),
            _=> query.OrderBy(x=>x.Name)
        };
        
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<string>> GetTypesAsync()
    {
        return await context.Products.Select(x=>x.Type).Distinct().ToListAsync();
    }

    public bool ProductExists(int id)
    {
        return context.Products.Any(x=> x.Id == id);
    }

    public async Task<bool> SaveChangessAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProdcut(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
