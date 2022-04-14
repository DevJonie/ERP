using ERP.SalesOrder.Contracts;

namespace ERP.SalesOrder.Entities;
public class SaleOrder
{
    public long Id { get; set; }
    public SalesOrderStatus Status { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerAddress { get; set; } = null!;
    public DateTime CreatedDate { get; set; }

    public List<OrderItem> OrderItems { get; set; } = new();
}



