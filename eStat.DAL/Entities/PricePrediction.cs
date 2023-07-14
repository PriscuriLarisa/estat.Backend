using System.ComponentModel.DataAnnotations;

namespace eStat.DAL.Entities
{
    public class PricePrediction
    {
        [Key]
        public Guid PricePredictionGUID { get; set; }
        public Guid UserProductID { get; set; }
        public int NbOfSearchesLastMonth { get; set; }
        public int NbOfPurchasesLastMonth { get; set; }
        public decimal CurrentAveragePrice { get; set; }
        public decimal AveragePriceLastMonth { get; set; }
        public decimal CurrentAverageStockPerRetailer { get; set; }
        public decimal AverageStockPerRetailerLastMonth { get; set; }
        public int MyStock { get; set; }
        public decimal MyCurrentPrice { get; set; }
        public decimal MyAveragePriceLastMonth { get; set; }
        public int MySellsLastMonth { get; set; }
        public decimal? PredictedPrice { get; set; }
    }
}
