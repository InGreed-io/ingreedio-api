using InGreedIoApi.Services;

namespace UnitTests;

public class PaginationServiceTests
{
    private readonly PaginationService _paginationService;

    public PaginationServiceTests()
    {
        _paginationService = new PaginationService();
    }

    [Fact]
    public async Task Paginate_FullPage_ReturnsPaginatedContent()
    {
        // Arrange
        IEnumerable<int> paginationData = Enumerable.Range(1, 100);
        int limit = 5;
        int page = 0;

        // Act
        var paginatedData = await _paginationService.Paginate<int>(paginationData, limit, page);

        // Assert
        Assert.NotNull(paginatedData);
        Assert.Equal(5, paginatedData.Content.Count());
        Assert.Equal(Enumerable.Range(1, 5), paginatedData.Content);
        Assert.Equal(limit, paginatedData.Limit);
        Assert.Equal(20, paginatedData.PageCount);
    }

    [Fact]
    public async Task Paginate_NonFullPage_ReturnsPaginatedLastPage()
    {
        // Arrange
        IEnumerable<int> paginationData = Enumerable.Range(1, 8);
        int limit = 5;
        int page = 1;

        // Act
        var paginatedData = await _paginationService.Paginate<int>(paginationData, limit, page);

        // Assert
        Assert.NotNull(paginatedData);
        Assert.Equal(3, paginatedData.Content.Count());
        Assert.Equal(Enumerable.Range(6, 3), paginatedData.Content);
        Assert.Equal(limit, paginatedData.Limit);
        Assert.Equal(2, paginatedData.PageCount);
    }

    [Fact]
    public async Task Paginate_LimitIsZero_ReturnsEmptyArrayAndNoPages()
    {
        // Arrange
        IEnumerable<int> paginationData = Enumerable.Range(1, 100);
        int limit = 0;
        int page = 0;

        // Act
        var paginatedData = await _paginationService.Paginate<int>(paginationData, limit, page);

        // Assert
        Assert.NotNull(paginatedData);
        Assert.Empty(paginatedData.Content);
        Assert.Equal(0, paginatedData.Limit);
        Assert.Equal(0, paginatedData.PageCount);
    }
}
