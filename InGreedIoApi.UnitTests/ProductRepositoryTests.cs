using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.DTO;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTests;

public class ProductRepositoryTests
{
    private readonly ApiDbContext _mockContext;
    private readonly IMapper _mockMapper;
    private readonly List<ProductPOCO> _products;

    public ProductRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _mockContext = new ApiDbContext(options);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ListMapper>();
            cfg.AddProfile<DTOMapper>();
            cfg.AddProfile<POCOMapper>();
        });
        _mockMapper = configuration.CreateMapper();

        _products = new List<ProductPOCO>
    {
        new ProductPOCO { Id = 1, Name = "Apple", CategoryId = 1, Description = "A sweet apple", IconUrl = "apple.jpg" },
        new ProductPOCO { Id = 2, Name = "Banana", CategoryId = 2, Description = "A ripe banana", IconUrl = "banana.jpg" }
    };

        _mockContext.Products.AddRange(_products);
        _mockContext.SaveChanges();
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task GetAll_ReturnsFilteredProducts()
    {
        // Arrange
        var queryDto = new ProductQueryDTO(query: "a", categoryId: null, ingredients: null, preferenceId: null, SortBy: null, pageIndex: 0, pageSize: 10);
        var repository = new ProductRepository(_mockMapper, _mockContext);

        // Act
        var result = await repository.GetAll(queryDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Contents.Count());
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task GetProduct_ExistingId_ReturnsProduct()
    {
        // Arrange
        var productId = 1;
        var repository = new ProductRepository(_mockMapper, _mockContext);

        // Act
        var result = await repository.GetProduct(productId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Apple", result.Name);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task AddReview_AddsSuccessfully()
    {
        // Arrange
        var productId = 1;
        var userId = "user123";
        var content = "Great apple!";
        var rating = 5.0f;
        var repository = new ProductRepository(_mockMapper, _mockContext);

        // Act
        var result = await repository.AddReview(productId, userId, content, rating);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, _mockContext.Reviews.Count());
        Assert.Equal(content, result.Text);
        Assert.Equal(rating, result.Rating);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Delete_ExistingProduct_ReturnsTrue()
    {
        // Arrange
        var productId = 1;
        var repository = new ProductRepository(_mockMapper, _mockContext);

        // Act
        var exception = await Record.ExceptionAsync(() => repository.Delete(productId));

        // Assert
        Assert.Null(exception);
        Assert.Equal(1, _mockContext.Products.Count());
    }
}