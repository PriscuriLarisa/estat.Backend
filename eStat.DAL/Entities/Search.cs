using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class Search
    {
        [Key]
        public Guid SearchGUID { get; set; }
        [ForeignKey("Product")]
        public Guid ProductGUID { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("User")]
        public Guid UserGUID { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
    }
}