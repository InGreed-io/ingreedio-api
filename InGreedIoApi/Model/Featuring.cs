namespace InGreedIoApi.Model
{
    public class Featuring
    {
        public int Id { get; set; }

        public DateTime? ExpireDate { get; set; }
        public bool PaymentConfirmed { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}