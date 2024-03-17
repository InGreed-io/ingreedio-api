using Microsoft.AspNetCore.Identity;
using InGreedIoApi.POCO;

namespace InGreedIoApi.Model
{
    public class ApiUser : IdentityUser
    {
        public bool IsBlocked { get; set; }
        public int? CompanyId { get; set; }
        public CompanyInfoPOCO? Company { get; set; }
        public ICollection<ProductPOCO> FavouriteProducts { get; set; }
        public ICollection<ProductPOCO> ProduceProducts { get; set; }
        public ICollection<AppNotificationPOCO> AppNotifications { get; set; }
        public ICollection<OperationLogPOCO> Operations { get; set; }
    }
}