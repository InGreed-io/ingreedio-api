namespace InGreedIoApi.Model
{
    public class Review
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public required float Rating { get; set; }
        public required int ProductId { get; set; }
        public Product Product { get; set; }
    }
}