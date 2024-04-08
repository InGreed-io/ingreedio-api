namespace InGreedIoApi.POCO
{
    public class PreferencePOCO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public ICollection<IngredientPOCO> Wanted { get; set; }
        public ICollection<IngredientPOCO> Unwanted { get; set; }
        public string UserId { get; set; }
        public ApiUserPOCO User { get; set; }
    }
}