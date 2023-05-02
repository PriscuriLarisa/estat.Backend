﻿using System.ComponentModel.DataAnnotations;

namespace eStat.DAL.Entities
{
    public class Product
    {
        [Key]
        public Guid ProductGUID { get; set; }
        public string Characteristics { get; set; }
        public bool InUse { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public decimal BasePrice { get; set; }
    }
}