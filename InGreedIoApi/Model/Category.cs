﻿using System.ComponentModel.DataAnnotations;

namespace InGreedIoApi.Model
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}