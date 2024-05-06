using InGreedIoApi.Model.Exceptions;
using InGreedIoApi.Utils.Pagination;

namespace UnitTests;

public class PaginationTests
{
    [Fact]
    public void Paginate_FullPage_ReturnsPaginatedContent()
    {
        // Arrange
        var paginationData = Enumerable.Range(1, 100);
        var pageIndex = 0;
        var pageSize = 5;

        // Act
        var paginatedData = paginationData.AsQueryable().ToPage(pageIndex, pageSize);

        // Assert
        Assert.Equal(Enumerable.Range(1, 5), paginatedData.Contents);
        Assert.Equal(0, paginatedData.Metadata.PageIndex);
        Assert.Equal(5, paginatedData.Metadata.PageSize);
        Assert.Equal(20, paginatedData.Metadata.PageCount);
    }

    [Fact]
    public void Paginate_NonFullPage_ReturnsPaginatedLastPage()
    {
        // Arrange
        var paginationData = Enumerable.Range(1, 8);
        var pageIndex = 1;
        var pageSize = 5;

        // Act
        var paginatedData = paginationData.AsQueryable().ToPage(pageIndex, pageSize);

        // Assert
        Assert.Equal(Enumerable.Range(6, 3), paginatedData.Contents);
        Assert.Equal(1, paginatedData.Metadata.PageIndex);
        Assert.Equal(5, paginatedData.Metadata.PageSize);
        Assert.Equal(2, paginatedData.Metadata.PageCount);
    }

    [Fact]
    public void Paginate_PageSizeIsZero_ThrowsInGreedIoException()
    {
        // Arrange
        var paginationData = Enumerable.Range(1, 100);
        var pageIndex = 0;
        var pageSize = 0;

        // Act & Assert
        Assert.Throws<InGreedIoException>(
            () => paginationData.AsQueryable().ToPage(pageIndex, pageSize)
        );
    }

    [Fact]
    public void Paginate_PageIndexIsNegative_ThrowsInGreedIoException()
    {
        // Arrange
        var paginationData = Enumerable.Range(1, 100);
        var pageIndex = -1;
        var pageSize = 10;

        // Act & Assert
        Assert.Throws<InGreedIoException>(
            () => paginationData.AsQueryable().ToPage(pageIndex, pageSize)
        );
    }
}
