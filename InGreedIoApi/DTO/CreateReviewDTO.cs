using InGreedIoApi.POCO;

namespace InGreedIoApi.DTO
{
    public class CreateReviewDTO
    {
        public string Text { get; set; }
        public float Rating { get; set; }
        public int ProductId { get; set; }
        public string UserID { get; set; }
    }
}