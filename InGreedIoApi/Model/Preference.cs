using InGreedIoApi.POCO;

namespace InGreedIoApi.Model
{
    public class Preference
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Ingredient> Wanted { get; set; }
        public ICollection<Ingredient> Unwanted { get; set; }
    }
}