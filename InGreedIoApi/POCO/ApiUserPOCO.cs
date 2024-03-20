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
    }
}