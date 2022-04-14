using ERP.Common;

namespace ERP.ProductCatalog.Entites;
public class Product
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public Money MinPrice { get; set; } = null!;
}
