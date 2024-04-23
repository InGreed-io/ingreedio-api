namespace InGreedIoApi.Utils.Pagination
{
    public record PageMetadata
    (
        int PageNumber,
        int PageSize,
        bool IsLast
    );
}
