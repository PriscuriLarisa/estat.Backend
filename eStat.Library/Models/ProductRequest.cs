namespace eStat.Library.Models
{
    public class ProductRequest
    {
        public Guid ProductRequestGUID { get; set; }
        public Guid ProductGUID { get; set; }
        public Product Product { get; set; }
        public Guid UserGUID { get; set; }
        public User User { get; set; }
        public float Price { get; set; }
        public DateTime Date { get; set; }
    }
}
