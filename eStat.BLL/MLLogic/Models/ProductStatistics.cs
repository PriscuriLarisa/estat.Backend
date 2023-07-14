namespace eStat.BLL.MLLogic.Models
{
    public class ProductStatistics
    {
        public int NbOfSearchesLastMonth { get; set; }
        public int NbOfPurchasesLastMonth { get; set; }
        public decimal AveragePriceLastMonth { get; set; }
        public decimal CurrentAveragePrice { get; set; }
        public int CurrentAverageStockPerRetailer { get; set; }
        public int AverageStockPerRetailerLastMonth { get; set; }
    }
}