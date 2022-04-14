using ERP.Common;

namespace ERP.SalesOrder.Contracts;
public class OrderItemViewModel
{
    public long ProductId { get; set; }
    public int Quantity { get; set; }
    public Money UnitPrice { get; set; } = null!;
}
