using AutoMapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.Data;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.DTO;
using System.Numerics;

namespace UnitTests;

public class PreferenceRepositoryTests
{
    private readonly ApiDbContext _mockContext;
    private readonly IMapper _mockMapper;
    private readonly List<PreferencePOCO> _preferences;
    private readonly List<IngredientPOCO> _ingredients;
    private readonly List<ApiUserPOCO> _users;

    public PreferenceRepositoryTests()
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

        _ingredients = new List<IngredientPOCO>
        {
            new IngredientPOCO { Id = 1, Name = "Ingredient 1", IconUrl = "Icon 1" },
            new IngredientPOCO { Id = 2, Name = "Ingredient 2", IconUrl = "Icon 2" }
        };
        _users = new List<ApiUserPOCO>
        {
            new ApiUserPOCO { Id = "User1", Email = "User1@a.a" }
        };

        _preferences = new List<PreferencePOCO>
        {
            new PreferencePOCO { Id = 1, Name = "Preference 1", IsActive = true, Wanted = _ingredients, UserId = "User1", User = _users[0] },
            new PreferencePOCO { Id = 2, Name = "Preference 2", IsActive = true, Wanted = new List<IngredientPOCO>(), UserId = "User1", User = _users[0] }
        };

        _mockContext.Preferences.AddRange(_preferences);
        _mockContext.Ingredients.AddRange(_ingredients);
        _mockContext.ApiUsers.AddRange(_users);

        _mockContext.SaveChanges();
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task DeleteIngredient_DeleteIngredientFromPreference_ReturnsTrue()
    {
        // Arrange
        var repository = new PreferenceRepository(_mockContext, _mockMapper);

        // Act
        var isDeleted = await repository.DeleteIngredient(1, 1);

        // Assert
        Assert.True(isDeleted);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Delete_DeletePreference_ReturnsTrue()
    {
        // Arrange
        var repository = new PreferenceRepository(_mockContext, _mockMapper);

        // Act
        var isDeleted = await repository.Delete(1);

        // Assert
        Assert.True(isDeleted);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public void FindById_FindIngredientFromPreferenceLists_ReturnsFoundIngredient()
    {
        // Arrange
        var repository = new PreferenceRepository(_mockContext, _mockMapper);

        // Act
        var ingredient = repository.FindById(_preferences[0], 1);

        // Assert
        Assert.NotNull(ingredient);
        Assert.Equal("Ingredient 1", ingredient.Name);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task AddIngredient_AddIngredientToPreferenceList_ReturnsTrue()
    {
        // Arrange
        var repository = new PreferenceRepository(_mockContext, _mockMapper);
        var addIngredientDTO = new AddIngredientDTO(1, true);
        // Act
        var isAdded = await repository.AddIngredient(2, addIngredientDTO);

        // Assert
        Assert.True(isAdded);
    }
}