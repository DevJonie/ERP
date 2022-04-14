using ERP.SalesOrder.Contracts;
using ERP.SalesOrder.Persistence;
using ERP.SalesOrder.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ERP.SalesOrders;
public static class ConfigureServices
{
    public static IServiceCollection AddSalesOrder(this IServiceCollection services, IConfiguration config)
    {
        var conString = config.GetConnectionString("SalesOrderDb");
        services.AddDbContextPool<SaleOrderDbContext>(options => 
            options.UseSqlServer(conString));

        services.AddTransient<ISalesOrderService, SalesOrderService>();

        services.AddTransient<IOrderItemsService, OrderItemsService>();

        return services;
    }

}
