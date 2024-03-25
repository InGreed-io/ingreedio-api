using InGreedIoApi.POCO;

namespace InGreedIoApi.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Text { get; set; }
        public float Rating { get; set; }
        public string UserID { get; set; }
    }
}
