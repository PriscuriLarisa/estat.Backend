namespace eStat.Library.Models
{
    public class ShoppingCart
    {
        public Guid ShoppingCartGUID { get; set; }
        public Guid? UserGUID { get; set; }
        public User? User { get; set; }
        public List<ShoppingCartProduct> Products { get; set; }
    }
}