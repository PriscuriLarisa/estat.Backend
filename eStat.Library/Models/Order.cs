namespace eStat.Library.Models
{
    public class Order
    {
        public Guid OrderGUID { get; set; }
        public Guid? UserGUID { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}
