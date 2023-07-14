namespace eStat.Library.Models
{
    public class PriceChangeCreate
    {
        public Guid UserProductGUID { get; set; }
        public Guid ProductGUID { get; set; }
        public decimal FromPrice { get; set; }
        public decimal ToPrice { get; set; }
    }
}