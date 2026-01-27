using Core.Entities;
namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brands, string? types, string? sort);
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<string>> GetBrandAsync();
    Task<IReadOnlyList<string>> GetTypesAsync();
    void AddProdcut(Product product);
    void UpdateProdcut(Product product);
    void DeleteProdcut(Product product);
    bool ProductExists(int id);
    Task<bool> SaveChangessAsync();
}
