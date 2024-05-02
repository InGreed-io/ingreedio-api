namespace InGreedIoApi.Utils.Pagination
{
    public record PageMetadata
    (
        int PageIndex,
        int PageSize,
        int PageCount
    );
}
