using ERP.Common;
using ERP.ProductCatalog.Contracts;
using Microsoft.Extensions.Caching.Distributed;

namespace ERP.ProductCatalog.Services;
public class ProductServiceCahceDecorator : IProductService
{
    const string cacheKey = "ErpProductsKey";

    private readonly IProductService _productService;
    private readonly IDistributedCache _cache;

    public ProductServiceCahceDecorator(
        IProductService prodService,
        IDistributedCache cache)
    {
        _productService = prodService;
        _cache = cache;
    }

    public async Task<ProductDto> AddAsync(ProductViewModel product)
    {
        var newProduct = await _productService.AddAsync(product);

        _ = await RefreshCache();

        return newProduct;
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync(IEnumerable<long> productIds)
    {
        var products = await GetProductsAsync();

        return products.Where(p => productIds.Contains(p.Id));
    }

    public async Task<IEnumerable<ProductDto>> GetAllAsync()
    {
        return await GetProductsAsync();
    }

    public async Task<ProductDto?> GetByIdAsync(long id)
    {
        var products = await GetProductsAsync();

        return products.SingleOrDefault(p => p.Id == id);
    }


    private async Task<IEnumerable<ProductDto>> GetProductsAsync()
    {
        var products = await _cache.GetAsync<IEnumerable<ProductDto>>(cacheKey);

        if (products is not null) return products;
        
        products = await _productService.GetAllAsync();

        await SetCacheAsync(products);

        return products;
    }

    private async Task SetCacheAsync(IEnumerable<ProductDto>? products)
    {
        if (products is null || !products.Any()) return;

        var cacheOptions = new DistributedCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(30));

        await _cache.SetAsync(cacheKey, products, cacheOptions);
    }

    private async Task<IEnumerable<ProductDto>> RefreshCache()
    {
        await _cache.RemoveAsync(cacheKey);

        var products = await _productService.GetAllAsync();

        await SetCacheAsync(products);

        return products;
    }
}
