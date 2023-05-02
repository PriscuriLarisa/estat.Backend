namespace eStat.Library.Models
{
    public class Purchase
    {
        public Guid PurchaseGUID { get; set; }
        public Guid? UserGUID { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public List<PurchaseProduct> Products { get; set; }
    }
}
