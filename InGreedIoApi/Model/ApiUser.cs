using Microsoft.AspNetCore.Identity;

namespace InGreedIoApi.Model
{
    public class ApiUser : IdentityUser
    {
        public required bool IsBlocked { get; set; }
        public int? CompanyId { get; set; }
        public CompanyInfo? Company { get; set; }
        public ICollection<Product> FavouriteProducts { get; set; }
    }
}