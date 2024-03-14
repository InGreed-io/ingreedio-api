using System.ComponentModel.DataAnnotations;

namespace InGreedIoApi.Model
{
    public class CompanyInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string NIP { get; set; }

        public ICollection<ApiUser> Users { get; set; }
    }
}