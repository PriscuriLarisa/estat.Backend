namespace eStat.Library.Models
{
    public class OrderProduct
    {
        public Guid OrderProductGUID { get; set; }
        public Guid? OrderGUID { get; set; }
        public Order? Order { get; set; }
        public Guid UserProductGUID { get; set; }
        public Product Product { get; set; }
        public float Price { get; set; }
    }
}
