using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eStat.DAL.Entities
{
    public class ShoppingCartProduct
    {
        [Key]
        public Guid ShoppingCartProductGUID { get; set; }
        [ForeignKey("ShoppingCart")]
        public Guid? ShoppingCartGUID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        [ForeignKey("UserProduct")]
        public Guid UserProductGUID { get; set; }
        public UserProduct? UserProduct { get; set; }
        public int Quantity { get; set; }
    }
}
