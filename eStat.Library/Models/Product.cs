namespace eStat.Library.Models
{
    public class Product
    {
        public Guid ProductGUID { get; set; }
        public string Characteristics { get; set; }
        public bool InUse { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public decimal BasePrice { get; set; }
    }
}