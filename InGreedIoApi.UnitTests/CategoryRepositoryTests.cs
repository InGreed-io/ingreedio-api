using AutoMapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.Data;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Mapper;

namespace UnitTests;

public class CategoryRepositoryTests
{
    private readonly ApiDbContext _mockContext;
    private readonly IMapper _mockMapper;
    private readonly List<CategoryPOCO> _categories;

    public CategoryRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _mockContext = new ApiDbContext(options);

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<POCOMapper>();
        });
        _mockMapper = configuration.CreateMapper();

        _categories = new List<CategoryPOCO>
        {
            new CategoryPOCO { Id = 1, Name = "Category 1" },
            new CategoryPOCO { Id = 2, Name = "Category 2" }
        };

        _mockContext.Category.AddRange(_categories);
        _mockContext.SaveChanges();
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task GetAll_ReturnsCorrectCategories_ReturnAllExistingCategories()
    {
        // Arrange
        var repository = new CategoryRepository(_mockMapper, _mockContext);

        // Act
        var categories = await repository.GetAll();

        // Assert
        Assert.NotNull(categories);
        Assert.Equal(2, categories.Count());
        Assert.Equal("Category 1", categories.First().Name);
    }
}