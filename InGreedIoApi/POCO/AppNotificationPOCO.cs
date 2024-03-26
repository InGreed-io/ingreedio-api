using InGreedIoApi.Model.Enum;
using InGreedIoApi.Model;

namespace InGreedIoApi.POCO
{
    public class AppNotificationPOCO
    {
        public int Id { get; set; }
        public DateTime? Seen { get; set; }
        public string UserId { get; set; }
        public ApiUserPOCO User { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}