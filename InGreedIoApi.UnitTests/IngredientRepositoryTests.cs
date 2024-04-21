using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class IngredientRepositoryTests
{
    private readonly ApiDbContext _mockContext;
    private readonly IMapper _mockMapper;
    private readonly List<IngredientPOCO> _ingredients;

    public IngredientRepositoryTests()
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

        _ingredients =
        [
            new IngredientPOCO { Id = 1, Name = "Ingredient 1", IconUrl = "Icon 1" },
            new IngredientPOCO { Id = 2, Name = "Ingredient 2", IconUrl = "Icon 2" },
            new IngredientPOCO { Id = 3, Name = "Something else", IconUrl = "Icon 3" }
        ];

        _mockContext.Ingredients.AddRange(_ingredients);
        _mockContext.SaveChanges();
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task FindAll_ReturnsCorrectIngredients_ReturnFirstPageOfSizeOne()
    {
        // Arrange
        var repository = new IngredientRepository(_mockMapper, _mockContext);
        var query = "Ingred";
        // Act
        var ingredients = await repository.FindAll(query);

        // Assert
        Assert.NotNull(ingredients);
        Assert.Equal(2, ingredients.Count());
        Assert.Equal("Ingredient 1", ingredients.First().Name);
    }
}
