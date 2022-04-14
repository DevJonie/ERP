using ERP.Common;
using ERP.ProductCatalog.Contracts;
using ERP.ProductCatalog.Entites;
using ERP.ProductCatalog.Persistence;
using ERP.ProductCatalog.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace ERP.ProductCatalog.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task GetAll_ShouldReturnEmptyIEnumerableOfProductDto_WhenDbHasNoItems()
    {
        //arrange
        var productService = CreateProductService("ProdCatDb1");

        //Act
        var products = await productService.GetAllAsync();

        //Assert
        products.Should().NotBeNull();
        products.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAll_ShouldReturnIEnumerableOfProductDto_WhenDbHasItems()
    {
        //arrange
        var productService = CreateProductService("ProdCatDb2", GetFakeProducts());

        //Act
        var products = await productService.GetAllAsync();

        //Assert
        products.Should().NotBeNullOrEmpty();
        products.Should().HaveCount(2);
    }

    [Fact]
    public async Task GetById_ShouldReturn_ProductDto_WhenProductExists()
    {
        //arrange
        var productService = CreateProductService("ProdCatDb3", GetFakeProducts());

        //Act
        var product = await productService.GetByIdAsync(2);

        //Assert
        product.Should().NotBeNull();
        product?.Id.Should().Be(2);
        product?.Name.Should().Be("Prod2");
        product?.MinPrice.Should().Be(new Money(2.2, "USD"));
    }

    [Fact]
    public async Task GetById_ShouldReturnNull_WhenProductDoesNotExist()
    {
        //arrange
        var productService = CreateProductService("ProdCatDb4", GetFakeProducts());

        //Act
        var product = await productService.GetByIdAsync(10);

        //Assert
        product.Should().BeNull();
    }

    private static IProductService CreateProductService(string dbName, List<Product>? products = null)
    {
        var options = new DbContextOptionsBuilder<ProductCatalogDbContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;

        var context = new ProductCatalogDbContext(options);
        if (products is not null)
        {
            context.Products.AddRangeAsync(products);
            context.SaveChanges();
        }

        IOptions<MemoryCacheOptions> option = new MemoryCacheOptions();
        IMemoryCache _cache = new MemoryCache(option);
        var productService = new ProductService(context, _cache);

        return productService;
    }

    private static List<Product> GetFakeProducts() 
    {
        return new()
        {
            new () { Id = 1, Name = "Prod1", MinPrice = new (1.1, "USD")},
            new () { Id = 2, Name = "Prod2", MinPrice = new (2.2, "USD")}
        };
    }

}