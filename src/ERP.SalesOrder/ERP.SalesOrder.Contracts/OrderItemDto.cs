using ERP.Common;

namespace ERP.SalesOrder.Contracts;
public class OrderItemDto
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public Money UnitPrice { get; set; } = null!;
    public double Cost 
    { 
        get { return UnitPrice.Amount * Quantity; }
    }
}
