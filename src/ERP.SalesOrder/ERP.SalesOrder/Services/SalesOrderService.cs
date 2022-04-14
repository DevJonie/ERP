using ERP.ProductCatalog.Contracts;
using ERP.SalesOrder.Contracts;
using ERP.SalesOrder.Entities;
using ERP.SalesOrder.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ERP.SalesOrder.Services;
public class SalesOrderService : ISalesOrderService
{
    private readonly SaleOrderDbContext _dbContxt;
    private readonly IProductService _service;

    public SalesOrderService(
        SaleOrderDbContext dbContxt,
        IProductService service)
    {
        _dbContxt = dbContxt;
        _service = service;
    }

    public async Task<SalesOrderDto?> AddAsync(SalesOrderViewModel salesOrder)
    {
        var newSalesOrder = await MapToSalesOrder(salesOrder);
        
        newSalesOrder.Status = SalesOrderStatus.Draft;

        _dbContxt.SalesOrders.Add(newSalesOrder);
        await _dbContxt.SaveChangesAsync();

        return newSalesOrder.Adapt<SalesOrderDto>();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var salesOrder = await _dbContxt.SalesOrders
            .SingleOrDefaultAsync(s => s.Id == id);

        if (salesOrder == null) return false;

        _dbContxt.SalesOrders.Remove(salesOrder);
        await _dbContxt.SaveChangesAsync();
        
        return true;
    }

    public async Task<IEnumerable<SalesOrderDto>> GetAllAsync(int pageIndex = 0, int pageSize = 50)
    {
        return await _dbContxt.SalesOrders
            .AsNoTracking()
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Include(s => s.OrderItems)
            .ProjectToType<SalesOrderDto>()
            .ToListAsync();
    }

    public async Task<SalesOrderDto?> GetById(long id)
    {
        var salesOrder = await _dbContxt.SalesOrders
            .AsNoTracking()
            .Include(s => s.OrderItems)
            .SingleOrDefaultAsync(s => s.Id == id);

        if (salesOrder is null) return null;

        return salesOrder.Adapt<SalesOrderDto>();
    }

    private async Task<SaleOrder> MapToSalesOrder(SalesOrderViewModel salesOrder)
    {
        var productIds = salesOrder.OrderItems
                    .Select(x => x.ProductId)
                    .ToList();
        var products = await _service.GetAllAsync(productIds);

        var newSalesOrder = salesOrder.Adapt<SaleOrder>();

        foreach (var orderItems in newSalesOrder.OrderItems)
        {
            var product = products.First(p => p.Id == orderItems.ProductId);

            orderItems.ProductName = product.Name;

            if (orderItems.UnitPrice.Amount <= product.MinPrice.Amount)
            {
                orderItems.UnitPrice = product.MinPrice;
            }
        }

        return newSalesOrder;
    }

}
