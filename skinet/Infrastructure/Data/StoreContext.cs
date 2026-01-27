using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastructure.Config;
namespace Infrastructure.Data;

public class StoreContext(DbContextOptions option) : DbContext(option)
{
    public DbSet<Product> Products {get;set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
    }
}