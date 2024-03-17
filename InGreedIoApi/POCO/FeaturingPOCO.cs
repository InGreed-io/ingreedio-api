namespace InGreedIoApi.POCO
{
    public class FeaturingPOCO
    {
        public int Id { get; set; }
        public DateTime? ExpireDate { get; set; }
        public bool PaymentConfirmed { get; set; }
        public int ProductId { get; set; }
        public ProductPOCO Product { get; set; }
    }
}