namespace InGreedIoApi.Utils.Pagination
{
    public class PaginationOptions
    {
        public const string SectionName = "Pagination";

        public bool MoveMetadataToHeader { get; set; } = false;
        public string PageIndexHeaderName { get; set; } = "page-index";
        public string PageSizeHeaderName { get; set; } = "page-size";
        public string PageCountHeaderName { get; set; } = "page-count";
    }
}
