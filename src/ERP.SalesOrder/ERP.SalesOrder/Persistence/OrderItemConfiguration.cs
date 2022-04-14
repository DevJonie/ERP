using ERP.SalesOrder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.SalesOrder.Persistence;
public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(o => o.ProductId)
            .IsRequired();

        builder.OwnsOne(o => o.UnitPrice, o => 
        {
            o.Property(u => u.Amount)
            .HasColumnName("UnitPrice")
            .HasPrecision(4)
            .HasDefaultValue(0);

            o.Property(u => u.Currency)
            .HasColumnName("Currency")
            .HasDefaultValue("USD");
        });
    }
}
