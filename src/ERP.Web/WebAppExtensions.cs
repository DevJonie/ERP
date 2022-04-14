using ERP.ProductCatalog.Persistence;
using ERP.SalesOrder.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERP.Web;

public static class WebAppExtensions
{
    public static void Migrate(this WebApplication app)
    {
        try
        {
            using var scope = app.Services.CreateScope();

            var prod_context = scope.ServiceProvider
                .GetRequiredService<ProductCatalogDbContext>();
            if (prod_context.Database.IsRelational())
            {
                prod_context.Database.Migrate();
            }

            var sales_context = scope.ServiceProvider
                .GetRequiredService<SaleOrderDbContext>();
            if (sales_context.Database.IsRelational())
            {
                sales_context.Database.Migrate();
            }
        }
        catch (Exception ex)
        {
            var logger = app.Services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
    }
}
