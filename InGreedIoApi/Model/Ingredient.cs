using System.ComponentModel.DataAnnotations;

namespace InGreedIoApi.Model
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public string IconUrl { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}