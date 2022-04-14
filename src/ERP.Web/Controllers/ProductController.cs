using ERP.ProductCatalog.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ERP.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet(Name = "GetProducts")]
    public async Task<IEnumerable<ProductDto>> Get()
    {
        return await _productService.GetAllAsync();
    }

    [HttpGet("{id}",Name = "GetProductById")]
    public async Task<ActionResult<ProductDto>> GetProductById(long id)
    {
        var product = await _productService.GetByIdAsync(id);

        return (product is null) ? NotFound() : Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDto>> Create(ProductViewModel model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var product = await _productService.AddAsync(model);

        if (product == null) return StatusCode(500);

        return CreatedAtAction("GetProductById", new { id = product.Id}, product);
    }
}
