using ERP.ProductCatalog.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace ERP.ProductCatalog.Persistence;
public class ProductCatalogDbContext : DbContext
{
    private readonly string _schemaName = "ProductCatalog";

    public ProductCatalogDbContext(
        DbContextOptions<ProductCatalogDbContext> options)
        :base(options)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.HasDefaultSchema(_schemaName);
        base.OnModelCreating(builder);
    }
}
