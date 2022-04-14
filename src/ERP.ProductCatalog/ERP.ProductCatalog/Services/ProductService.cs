using ERP.ProductCatalog.Contracts;
using ERP.ProductCatalog.Entites;
using ERP.ProductCatalog.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace ERP.ProductCatalog.Services;
public class ProductService : IProductService
{
    const string cacheKey = "ErpProductsKey";

    private readonly ProductCatalogDbContext _dbContext;
    private readonly IMemoryCache _cache;

    public ProductService(
        ProductCatalogDbContext dbContext,
        IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<ProductDto> AddAsync(ProductViewModel product)
    {
        var newProduct = product.Adapt<Product>();

        await _dbContext.Products.AddAsync(newProduct);
        await _dbContext.SaveChangesAsync();

        _cache.Remove(cacheKey);

        return newProduct.Adapt<ProductDto>();
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync() 
    {
        if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto>  products))
        {
            return products;
        }

        products = await _dbContext.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToListAsync();

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30));
        _cache.Set(cacheKey, products, cacheOptions);

        return products;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(IEnumerable<long> productIds)
    {
        if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> products))
        {
            return products.Where(p => productIds.Contains(p.Id));
        }

        products = await _dbContext.Products
            .AsNoTracking()
            .Where(p => productIds.Contains(p.Id))
            .ProjectToType<ProductDto>()
            .ToListAsync();

        return products;
    }

    public async Task<ProductDto?> GetByIdAsync(long id)
    {
        if (_cache.TryGetValue(cacheKey, out IEnumerable<ProductDto> products))
        {
            return products.SingleOrDefault(p => p.Id == id);
        }

        var product = await _dbContext.Products
            .AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == id);

        if(product == null) return null;

        return product.Adapt<ProductDto>();
    }
}
