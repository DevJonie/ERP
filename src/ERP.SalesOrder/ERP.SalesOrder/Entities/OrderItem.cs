using ERP.Common;

namespace ERP.SalesOrder.Entities;
public class OrderItem
{
    public long Id { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public Money UnitPrice { get; set; } = null!;

    public long ProductId { get; set; }
    public long SalesOrderId { get; set; }
    public SaleOrder SalesOrder { get; set; } = null!;
}
