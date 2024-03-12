namespace InGreedIoApi.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public bool IsBlocked { get; set; }
        public int? CompanyId { get; set; }
        public CompanyInfo? Company { get; set; }
        public ICollection<Product> FavouriteProducts { get; set; }
    }
}