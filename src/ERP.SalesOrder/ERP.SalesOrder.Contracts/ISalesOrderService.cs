namespace ERP.SalesOrder.Contracts;
public interface ISalesOrderService
{
    Task<SalesOrderDto?> AddAsync(SalesOrderViewModel salesOrder);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<SalesOrderDto>> GetAllAsync(int pageIndex = 0, int pageSize = 50);
    Task<SalesOrderDto?> GetById(long id);
}
