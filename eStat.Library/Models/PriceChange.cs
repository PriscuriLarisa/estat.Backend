namespace eStat.Library.Models
{
    public class PriceChange
    {
        public Guid PriceChangeGUID { get; set; }
        public DateTime Date { get; set; }
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
        public Guid UserProductGUID { get; set; }
        public UserProduct? UserProduct { get; set; }
        public Guid ProductGUID { get; set; }
        public Product? Product { get; set; }
    }
}