using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InGreedIoApi.Model
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string IconUrl { get; set; }

        public string Description { get; set; }

        [ForeignKey("Featuring")]
        public int? FeaturingId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Featuring? Featuring { get; set; }
    }
}