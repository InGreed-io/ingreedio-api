using Microsoft.AspNetCore.Identity;

namespace InGreedIoApi.POCO
{
    public class ApiUserPOCO : IdentityUser
    {
        public bool IsBlocked { get; set; }
        public int? CompanyId { get; set; }
        public CompanyInfoPOCO? Company { get; set; }
        public ICollection<ProductPOCO> FavouriteProducts { get; set; }
        public ICollection<ProductPOCO> ProduceProducts { get; set; }
        public ICollection<ReviewPOCO> Reviews { get; set; }

        public ICollection<PreferencePOCO> Preferences { get; set; }
        public ICollection<AppNotificationPOCO> AppNotifications { get; set; }
        public ICollection<OperationLogPOCO> Operations { get; set; }
    }
}