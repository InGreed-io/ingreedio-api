namespace InGreedIoApi.Model
{
    public class Featuring
    {
        public int Id { get; set; }
        public DateTime? ExpireDate { get; set; }
        public required bool PaymentConfirmed { get; set; }
        public required int ProductId { get; set; }
        public Product Product { get; set; }
    }
}