using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.DAL.Entities
{
    public class UserProduct
    {
        [Key]
        public Guid UserProductGUID { get; set; }
        [ForeignKey("User")]
        public Guid UserGUID { get; set; }
        public User? User { get; set; }
        [ForeignKey("Product")]
        public Guid ProductGUID { get; set; }
        public Product? Product { get; set; } 
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}