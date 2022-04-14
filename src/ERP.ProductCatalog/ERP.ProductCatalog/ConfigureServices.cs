using ERP.ProductCatalog.Contracts;
using ERP.ProductCatalog.Persistence;
using ERP.ProductCatalog.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERP.ProductCatalog;
public static class ConfigureServices
{
    public static IServiceCollection AddProductCatalog(this IServiceCollection services, IConfiguration config)
    {
        var conString = config.GetConnectionString("ProductsCatalogDb");
        services.AddDbContext<ProductCatalogDbContext>(options =>
            options.UseSqlServer(conString));

        services.AddTransient<IProductService, ProductService>();

        return services;
    }
}
