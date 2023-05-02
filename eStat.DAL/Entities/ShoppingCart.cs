using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace eStat.DAL.Entities
{
    public class ShoppingCart
    {
        [Key]
        public Guid ShoppingCartGUID { get; set; }
        [ForeignKey("User")]
        public Guid? UserGUID { get; set; }
        public User? User { get; set; }
        public List<ShoppingCartProduct> Products { get; set; }
    }
}