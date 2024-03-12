namespace InGreedIoApi.Model
{
    public class CompanyInfo
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string NIP { get; set; }
        public ICollection<ApiUser> Users { get; set; }
    }
}