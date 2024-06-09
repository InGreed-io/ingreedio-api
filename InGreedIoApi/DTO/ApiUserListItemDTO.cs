namespace InGreedIoApi.DTO
{
    public class ApiUserListItemDTO
    {
        public string? Id { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public bool IsBlocked { get; set; }
    }
}
