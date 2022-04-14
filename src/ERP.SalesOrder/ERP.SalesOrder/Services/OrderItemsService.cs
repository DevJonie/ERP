using ERP.SalesOrder.Contracts;
using ERP.SalesOrder.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ERP.SalesOrder.Services;
public class OrderItemsService : IOrderItemsService
{
    private readonly  SaleOrderDbContext _dbContext;
    public OrderItemsService(SaleOrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<OrderItemDto>> GetAllAsync()
    {
        return await _dbContext.OrderItems
            .AsNoTracking()
            .ProjectToType<OrderItemDto>()
            .ToListAsync();
    }
}
