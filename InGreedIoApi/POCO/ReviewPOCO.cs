namespace InGreedIoApi.POCO
{
    public class ReviewPOCO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public float Rating { get; set; }
        public int ProductId { get; set; }
        public ProductPOCO Product { get; set; }
        public string UserID { get; set; }
        public ApiUserPOCO User { get; set; }
    }
}