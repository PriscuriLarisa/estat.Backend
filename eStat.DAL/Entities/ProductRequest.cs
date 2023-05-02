using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class ProductRequest
    {
        [Key]
        public Guid ProductRequestGUID { get; set; }
        [ForeignKey("User")]
        public Guid UserGUID { get; set; }
        public User User { get; set; }
        [ForeignKey("Product")]
        public Guid ProductGUID { get; set; }
        public Product Product { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }

    }
}
