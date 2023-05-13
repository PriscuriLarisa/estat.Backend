namespace eStat.Library.Models
{
    public class PurchaseProduct
    {
        public Guid PurchaseProductGUID { get; set; }
        public Guid PurchaseGUID { get; set; }
        public Guid UserProductGUID { get; set; }
        public UserProduct Product { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }
    }
}