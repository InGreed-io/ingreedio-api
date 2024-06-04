using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.DTO;
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
            cfg.AddProfile<ListMapper>();
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
        var query = new GetIngredientsQuery("Ingred", null, null, 0, 10);
        // Act
        var ingredients = await repository.FindAll(query);

        // Assert
        Assert.NotNull(ingredients.Contents);
        Assert.Equal(2, ingredients.Contents.Count());
        Assert.Equal("Ingredient 1", ingredients.Contents.First().Name);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task FindAll_QueryIngredIncludeId1_ReturnOnlyIngredientWithId1()
    {
        // Arrange
        var repository = new IngredientRepository(_mockMapper, _mockContext);
        var query = new GetIngredientsQuery("Ingred", [1], null, 0, 10);
        // Act
        var ingredients = await repository.FindAll(query);

        // Assert
        Assert.NotNull(ingredients.Contents);
        Assert.Single(ingredients.Contents);
        Assert.Equal(1, ingredients.Contents.First().Id);
        Assert.Equal("Ingredient 1", ingredients.Contents.First().Name);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task FindAll_QueryIngredExcludeId1_ReturnOnlyIngredientWithId1()
    {
        // Arrange
        var repository = new IngredientRepository(_mockMapper, _mockContext);
        var query = new GetIngredientsQuery("Ingred", null, [1], 0, 10);
        // Act
        var ingredients = await repository.FindAll(query);

        // Assert
        Assert.NotNull(ingredients.Contents);
        Assert.Single(ingredients.Contents);
        Assert.Equal(2, ingredients.Contents.First().Id);
        Assert.Equal("Ingredient 2", ingredients.Contents.First().Name);
    }
}
