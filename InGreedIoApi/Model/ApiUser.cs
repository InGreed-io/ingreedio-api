using Microsoft.AspNetCore.Identity;

namespace InGreedIoApi.Model
{
    public class ApiUser : IdentityUser
    {
        public bool IsBlocked { get; set; }
        public int? CompanyId { get; set; }
        public CompanyInfo? Company { get; set; }
        public ICollection<Product> FavouriteProducts { get; set; }
        public ICollection<Product> ProduceProducts { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Preference> Preferences { get; set; }
        public ICollection<AppNotification> AppNotifications { get; set; }
        public ICollection<OperationLog> Operations { get; set; }
    }
}
