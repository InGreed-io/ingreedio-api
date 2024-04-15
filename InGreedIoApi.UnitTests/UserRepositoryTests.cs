using AutoMapper;
using InGreedIoApi.Data;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;

namespace UnitTests;

public class UserRepositoryTests
{
    private readonly ApiDbContext _mockContext;
    private readonly IMapper _mockMapper;
    private readonly List<ApiUserPOCO> _users;
    private readonly List<PreferencePOCO> _preferences;

    public UserRepositoryTests()
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

        _preferences = new List<PreferencePOCO>
        {
            new PreferencePOCO { Id = 1, Name = "Preference 1", UserId = "User1" }
        };

        _users = new List<ApiUserPOCO>
        {
            new ApiUserPOCO { Id = "User1", Email = "User1@a.a", IsBlocked = false, Preferences = _preferences },
            new ApiUserPOCO { Id = "User2", Email = "User2@a.a", IsBlocked = false }
        };

        _mockContext.ApiUsers.AddRange(_users);
        _mockContext.Preferences.AddRange(_preferences);
        _mockContext.SaveChanges();
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task GetUserById_ReturnsCorrectUsers_ReturnUserWithProperId()
    {
        // Arrange
        var repository = new UserRepository(_mockContext, _mockMapper);

        // Act
        var user = await repository.GetUserById("User1");

        // Assert
        Assert.NotNull(user);
        Assert.Equal("User1@a.a", user.Email);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task GetPreferences_ReturnsCorrectPreferencesOfUser_ReturnPreferencesForUserId()
    {
        // Arrange
        var repository = new UserRepository(_mockContext, _mockMapper);

        // Act
        var preferences = await repository.GetPreferences("User1");

        // Assert
        Assert.NotNull(preferences);
        Assert.Single(preferences);
        Assert.Equal("Preference 1", preferences.First().Name);
    }
}