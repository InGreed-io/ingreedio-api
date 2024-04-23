namespace InGreedIoApi.Utils.Pagination
{
    public class PaginationOptions
    {
        public const string SectionName = "Pagination";

        public bool MoveMetadataToHeader { get; set; } = false;
        public string PageNumberHeaderName { get; set; } = "PageNumber";
        public string PageSizeHeaderName { get; set; } = "PageSize";
        public string IsLastHeaderName { get; set; } = "IsLast";
    }
}
