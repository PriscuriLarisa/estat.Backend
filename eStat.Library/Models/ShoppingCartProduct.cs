namespace eStat.Library.Models
{
    public class ShoppingCartProduct
    {
        public Guid ShoppingCartProductGUID { get; set; }
        public Guid? ShoppingCartGUID { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
        public Guid UserProductGUID { get; set; }
        public UserProduct? UserProduct { get; set; }
        public int Quantity { get; set; }
    }
}
