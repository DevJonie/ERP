using ERP.SalesOrder.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemsService _orderService;
    public OrderItemController(IOrderItemsService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet(Name = "GetOrderItems")]
    public async Task<IEnumerable<OrderItemDto>> Get()
    {
        return await _orderService.GetAllAsync();
    }

}
