using ERP.SalesOrder.Contracts;
using ERP.SalesOrder.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ERP.SalesOrder.Persistence;
public class SaleOrderConfiguration : IEntityTypeConfiguration<SaleOrder>
{
    public void Configure(EntityTypeBuilder<SaleOrder> builder)
    {
        builder.HasMany(s => s.OrderItems)
            .WithOne(o => o.SalesOrder)
            .HasForeignKey(o => o.SalesOrderId);

        builder.Property(s => s.CreatedDate)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(s => s.Status)
            .HasConversion(s => s.ToString(), s => Enum.Parse<SalesOrderStatus>(s));

        //List<OrderItem> items1 = new()
        //{
        //    new OrderItem { ProductName = "Laptop", Quantity = 2, UnitPrice = new(1200M, "USD") },
        //    new OrderItem { ProductName = "Mouse", Quantity = 3, UnitPrice = new(35M, "USD") },
        //    new OrderItem { ProductName = "Paper", Quantity = 5, UnitPrice = new(5M, "USD") },
        //};

        //List<OrderItem> items2 = new()
        //{
        //    new OrderItem { ProductName = "Monitor", Quantity = 3, UnitPrice = new(800M, "USD") },
        //    new OrderItem { ProductName = "Laptop", Quantity = 1, UnitPrice = new(1200M, "USD") },
        //};

        //builder.HasData(
        //    new SaleOrder { CreatedDate = DateTime.UtcNow, CustomerName = "William", OrderItems = items1},
        //    new SaleOrder { CreatedDate = DateTime.UtcNow, CustomerName = "Jasmine", OrderItems = items2}
        //);


    }
}
