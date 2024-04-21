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
    public async Task Paginate_ReturnsPaginatedContent()
    {
        // Arrange
        IEnumerable<int> paginationData = Enumerable.Range(1, 100);
        int limit = 5;
        int page = 0;
        int expectedPageCount = (int)Math.Ceiling((double)paginationData.Count() / (double)limit);

        // Act
        var paginatedData = await _paginationService.Paginate<int>(paginationData, limit, page);

        // Assert
        Assert.NotNull(paginatedData);
        Assert.Equal(5, paginatedData.Content.Count());
        Assert.Equal(Enumerable.Range(1, 5), paginatedData.Content);
        Assert.Equal(limit, paginatedData.Limit);
        Assert.Equal(expectedPageCount, paginatedData.PageCount);
    }

    [Fact]
    public async Task Paginate_ReturnsPaginatedContent_LastPage()
    {
        // Arrange
        IEnumerable<int> paginationData = Enumerable.Range(1, 8);
        int limit = 5;
        int page = 1;
        int expectedPageCount = (int)Math.Ceiling((double)paginationData.Count() / (double)limit);

        // Act
        var paginatedData = await _paginationService.Paginate<int>(paginationData, limit, page);

        // Assert
        Assert.NotNull(paginatedData);
        Assert.Equal(3, paginatedData.Content.Count());
        Assert.Equal(Enumerable.Range(6, 3), paginatedData.Content);
        Assert.Equal(limit, paginatedData.Limit);
        Assert.Equal(expectedPageCount, paginatedData.PageCount);
    }

    [Fact]
    public async Task Paginate_ReturnsPaginatedContent_LimitIsZero()
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
