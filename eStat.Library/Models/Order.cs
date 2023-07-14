namespace eStat.Library.Models
{
    public class Order
    {
        public Guid OrderGUID { get; set; }
        public Guid? UserGUID { get; set; }
        public UserInfo? User { get; set; }
        public DateTime Date { get; set; }
        public List<OrderProduct> Products { get; set; }
        public string Address { get; set; }
    }
}
