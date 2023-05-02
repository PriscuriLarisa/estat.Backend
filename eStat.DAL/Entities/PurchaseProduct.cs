using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class PurchaseProduct
    {
        [Key]
        public Guid PurchaseProductGUID { get; set; }
        [ForeignKey("Purchase")]
        public Guid PurchaseGUID { get; set; }
        public Purchase Purchase { get; set; }
        [ForeignKey("UserProduct")]
        public Guid UserProductGUID { get; set; }
        public UserProduct UserProduct { get; set; }
        public float Price { get; set; }
    }
}