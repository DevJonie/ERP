using Microsoft.EntityFrameworkCore;
using ERP.SalesOrder.Entities;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace ERP.SalesOrder.Persistence;
public class SaleOrderDbContext : DbContext
{
    private readonly string _schemaName = "SalesOrder";
    private readonly IConfiguration _config;

    public SaleOrderDbContext(
        DbContextOptions<SaleOrderDbContext> options)
        : base(options)
    {
    }

    public DbSet<SaleOrder> SalesOrders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        builder.HasDefaultSchema(_schemaName);
        base.OnModelCreating(builder);
    }

}
