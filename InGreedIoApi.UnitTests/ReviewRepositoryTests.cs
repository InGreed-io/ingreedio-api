using AutoMapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.Data;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Moq;

public class ReviewRepositoryTests
{
    private readonly ApiDbContext _mockContext;
    private readonly IMapper _mockMapper;
    private readonly List<ReviewPOCO> _reviews;

    public ReviewRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<ApiDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _mockContext = new ApiDbContext(options);

        _mockMapper = new Mock<IMapper>().Object;
        _reviews = new List<ReviewPOCO>
        {
            new ReviewPOCO { Id = 1, Text = "Review 1", UserID = "User1", Rating = 5 },
            new ReviewPOCO { Id = 2, Text = "Review 2", UserID = "User2", Rating = 4 }
        };

        _mockContext.Reviews.AddRange(_reviews);
        _mockContext.SaveChanges();
    }

    [Fact]
    public async Task GetAll_ReturnsCorrectReviews_ForGivenUserId()
    {
        // Arrange
        var userId = "User1";
        var repository = new ReviewRepository(_mockContext, _mockMapper);

        // Act
        var reviews = await repository.GetAll(userId);

        // Assert
        Assert.NotNull(reviews);
        Assert.Single(reviews);
        Assert.Equal("Review 1", reviews.First().Text);
    }
}