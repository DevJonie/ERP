using ERP.ProductCatalog.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.ProductCatalog.Persistence;
public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        //builder.HasData(
        //    new() { Id = 1, Name = "Laptop", Price = new(1200M, "USD") },
        //    new() { Id = 2, Name = "paper", Price = new(5M, "USD") },
        //    new() { Id = 3, Name = "Mouse", Price = new(35M, "USD") },
        //    new() { Id = 4, Name = "keyboard", Price = new(35M, "USD") },
        //    new() { Id = 5, Name = "Monitor", Price = new(800M, "USD") }
        //);

        builder.OwnsOne(p => p.MinPrice, p =>
        {
            p.Property(m => m.Amount)
            .HasColumnName("Price")
            .HasPrecision(4)
            .HasDefaultValue(0);

            p.Property(m => m.Currency)
            .HasColumnName("Currency")
            .HasConversion(c => c.ToUpper(),c => c.ToUpper())
            .HasDefaultValue("USD");
        });
    }
}
