﻿using System.ComponentModel.DataAnnotations;

namespace InGreedIoApi.Model
{
    public class CompanyInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        public string NIP { get; set; }

        public ICollection<ApiUser> Users { get; set; }
    }
}