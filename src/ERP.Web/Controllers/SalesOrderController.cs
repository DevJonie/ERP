using ERP.SalesOrder.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalesOrderController : ControllerBase
{
    private readonly ISalesOrderService _salesOrderService;

    public SalesOrderController(ISalesOrderService salesOrderService)
    {
        _salesOrderService = salesOrderService;
    }

    [HttpGet]
    public async Task<IEnumerable<SalesOrderDto>> Get()
    {
        return await _salesOrderService.GetAllAsync();
    }

    [HttpGet("{id}",Name = "GetSalesOrderById")]
    public async Task<ActionResult<SalesOrderDto>> GetbyId(long id)
    {
        var salesOrder = await _salesOrderService.GetById(id);

        return (salesOrder is null) ? NotFound() : Ok(salesOrder);
    }

    [HttpPost]
    public async Task<ActionResult<SalesOrderDto>> Create(SalesOrderViewModel model)
    {

        if (!ModelState.IsValid || !model.OrderItems.Any()) return BadRequest();

        var newSalesOrder = await _salesOrderService.AddAsync(model);

        return CreatedAtRoute("GetSalesOrderById", new { id = newSalesOrder?.Id }, newSalesOrder);
    }

    //[HttpDelete("{id}")]
    //public async Task<ActionResult<SalesOrderDto>> Delete(long id)
    //{
    //    var deleted = await _salesOrderService.DeleteAsync(id);

    //    return deleted ? NoContent() : NotFound();
    //}

}
