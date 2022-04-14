namespace ERP.ProductCatalog.Contracts;
public interface IProductService
{
    Task<ProductDto> AddAsync(ProductViewModel product);
    Task<IEnumerable<ProductDto>> GetAllAsync(IEnumerable<long> productIds);
    Task<IEnumerable<ProductDto>> GetAllAsync();
    Task<ProductDto?> GetByIdAsync(long id);
}
