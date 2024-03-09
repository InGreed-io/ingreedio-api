namespace InGreedIoApi.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public float Rating { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}