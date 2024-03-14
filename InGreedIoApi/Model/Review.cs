using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InGreedIoApi.Model
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        public string Text { get; set; }

        [Required]
        public float Rating { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}