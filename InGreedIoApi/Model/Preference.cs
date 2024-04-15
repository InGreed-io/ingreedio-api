namespace InGreedIoApi.Model
{
    public class Preference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Ingredient> Wanted { get; set; }
        public ICollection<Ingredient> Unwanted { get; set; }
        public string UserId { get; set; }
        public ApiUser User { get; set; }
    }
}