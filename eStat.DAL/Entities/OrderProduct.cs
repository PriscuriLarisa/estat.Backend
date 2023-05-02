using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class OrderProduct
    {
        [Key]
        public Guid OrderProductGUID { get; set; }
        [ForeignKey("Order")]
        public Guid? OrderGUID { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("Product")]
        public Guid ProductGUID { get; set; }
        public Product Product { get; set; }
        public float Price { get; set; }
    }
}
