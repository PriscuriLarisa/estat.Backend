namespace eStat.Library.Models
{
    public class ShoppingCartProductAdd
    {
        public Guid? ShoppingCartGUID { get; set; }
        public Guid UserProductGUID { get; set; }
        public int Quantity { get; set; }

    }
}
