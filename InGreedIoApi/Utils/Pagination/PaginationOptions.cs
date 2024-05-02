namespace InGreedIoApi.Utils.Pagination
{
    public class PaginationOptions
    {
        public const string SectionName = "Pagination";

        public bool MoveMetadataToHeader { get; set; } = false;
        public string PageIndexHeaderName { get; set; } = "PageIndex";
        public string PageSizeHeaderName { get; set; } = "PageSize";
        public string PageCountHeaderName { get; set; } = "PageCount";
    }
}
