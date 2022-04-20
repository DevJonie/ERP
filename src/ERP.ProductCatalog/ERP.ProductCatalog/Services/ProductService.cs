using ERP.Common;
using ERP.ProductCatalog.Contracts;
using ERP.ProductCatalog.Entites;
using ERP.ProductCatalog.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ERP.ProductCatalog.Services;
public class ProductService : IProductService
{
    private readonly ProductCatalogDbContext _dbContext;

    public ProductService(
        ProductCatalogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ProductDto> AddAsync(ProductViewModel product)
    {
        var newProduct = product.Adapt<Product>();

        await _dbContext.Products.AddAsync(newProduct);
        await _dbContext.SaveChangesAsync();

        return newProduct.Adapt<ProductDto>();
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        var products = await _dbContext.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync();

        return products;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(IEnumerable<long> productIds)
    {
        var products = await _dbContext.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ProjectToType<ProductDto>()
            .ToListAsync();

        return products;
    }

    public async Task<ProductDto?> GetByIdAsync(long id)
    {
        var product = await _dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        if(product == null) return null;

        return product.Adapt<ProductDto>();
    }
}
