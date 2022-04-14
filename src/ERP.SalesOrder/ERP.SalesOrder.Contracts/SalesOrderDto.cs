namespace ERP.SalesOrder.Contracts;
public class SalesOrderDto
{
    public long Id { get; set; }
    public SalesOrderStatus Status { get; set; }
    public string CustomerName { get; set; } = null!;
    public string CustomerAddress { get; set; } = null!;
    public DateTime CreatedDate { get; set; }

    public List<OrderItemDto> OrderItems { get; set; } = new();

    public string TotalCost => $"{ OrderItems.Sum(o => o.Cost)} {OrderItems.First().UnitPrice.Currency}";
    
}
