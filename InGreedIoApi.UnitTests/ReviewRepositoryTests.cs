using AutoMapper;
using InGreedIoApi.Data.Repository;
using InGreedIoApi.Data;
using InGreedIoApi.POCO;
using Microsoft.EntityFrameworkCore;
using Moq;
using InGreedIoApi.Data.Mapper;
using InGreedIoApi.DTO;
using System.ComponentModel;

namespace UnitTests;
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

        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<POCOMapper>();
        });
        _mockMapper = configuration.CreateMapper();

        _reviews = new List<ReviewPOCO>
        {
            new ReviewPOCO { Id = 1, Text = "Review 1", UserID = "User1", Rating = 5 },
            new ReviewPOCO { Id = 2, Text = "Review 2", UserID = "User2", Rating = 4 }
        };

        _mockContext.Reviews.AddRange(_reviews);
        _mockContext.SaveChanges();
    }

    [Trait("Category", "Unit")]
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

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Report_ExistingReview_ShouldIncrementReportsCount()
    {
        // Arrange
        int reviewId = 1;
        var repository = new ReviewRepository(_mockContext, _mockMapper);

        // Act
        var review = await repository.Report(reviewId);

        // Assert
        Assert.NotNull(review);
        Assert.Equal(1, review.ReportsCount);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Rate_ExistingReview_ShouldUpdateRating()
    {
        // Arrange
        var repository = new ReviewRepository(_mockContext, _mockMapper);
        var reviewId = 1;
        var newRating = 3.5f;

        // Act
        var result = await repository.Rate(reviewId, newRating);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newRating, result.Rating);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Update_ExistingReview_ShouldUpdateContentAndRating()
    {
        // Arrange
        var repository = new ReviewRepository(_mockContext, _mockMapper);
        var reviewId = 1;
        var updateDto = new ReviewUpdateDTO("Updated review content", 4.5f);

        // Act
        var result = await repository.Update(reviewId, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updateDto.Content, result.Text);
        Assert.Equal(updateDto.Rating, result.Rating);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Report_NonExistingReview_ShouldReturnNull()
    {
        // Arrange
        var repository = new ReviewRepository(_mockContext, _mockMapper);
        var nonExistingReviewId = 999;

        // Act
        var result = await repository.Report(nonExistingReviewId);

        // Assert
        Assert.Null(result);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Rate_NonExistingReview_ShouldReturnNull()
    {
        // Arrange
        var repository = new ReviewRepository(_mockContext, _mockMapper);
        var nonExistingReviewId = 999;
        var newRating = 3.5f;

        // Act
        var result = await repository.Rate(nonExistingReviewId, newRating);

        // Assert
        Assert.Null(result);
    }

    [Trait("Category", "Unit")]
    [Fact]
    public async Task Update_NonExistingReview_ShouldReturnNull()
    {
        // Arrange
        var repository = new ReviewRepository(_mockContext, _mockMapper);
        var nonExistingReviewId = 999;
        var updateDto = new ReviewUpdateDTO("Updated review content", 4.5f);

        // Act
        var result = await repository.Update(nonExistingReviewId, updateDto);

        // Assert
        Assert.Null(result);
    }
}