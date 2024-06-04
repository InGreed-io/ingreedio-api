namespace InGreedIoApi.DTO
{
    public class ApiUserListItemDTO(string Id, string Email, bool IsBlocked)
    {
        public string Id { get; set; } = Id;
        public string Email { get; set; } = Email;
        public string Role { get; set; } = string.Empty;
        public bool IsBlocked { get; set; } = IsBlocked;
    }
}
