using ERP.Common;

namespace ERP.ProductCatalog.Contracts;
public class ProductViewModel
{
    public string Name { get; set; } = null!;
    public Money MinPrice { get; set; } = null!;
}
