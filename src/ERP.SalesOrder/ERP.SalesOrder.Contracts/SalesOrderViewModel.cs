namespace ERP.SalesOrder.Contracts;
public class SalesOrderViewModel
{
    public string CustomerName { get; set; } = null!;
    public string CustomerAddress { get; set; } = null!;

    public List<OrderItemViewModel> OrderItems { get; set; } = new();
}
