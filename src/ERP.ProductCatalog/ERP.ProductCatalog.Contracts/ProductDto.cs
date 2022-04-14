using ERP.Common;

namespace ERP.ProductCatalog.Contracts;

public class ProductDto
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public Money MinPrice { get; set; } = null!;
}
