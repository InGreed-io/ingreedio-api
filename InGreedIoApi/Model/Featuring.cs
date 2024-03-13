using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InGreedIoApi.Model
{
    public class Featuring
    {
        [Key]
        public int Id { get; set; }

        public DateTime? ExpireDate { get; set; }
        public bool PaymentConfirmed { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}