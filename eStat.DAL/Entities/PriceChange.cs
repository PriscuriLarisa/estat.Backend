using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class PriceChange
    {
        [Key]
        public Guid PriceChangeGUID { get; set; }
        public DateTime Date { get; set; }
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
        [ForeignKey("UserProduct")]
        public Guid UserProductGUID { get; set; }
        public UserProduct? UserProduct { get; set; }
        [ForeignKey("Product")]
        public Guid ProductGUID { get; set; }
        public Product? Product { get; set; }
    }
}