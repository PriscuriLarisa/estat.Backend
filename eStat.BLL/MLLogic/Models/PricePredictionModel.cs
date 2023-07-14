using eStat.DAL.Implementations;
using System.Collections.Generic;

namespace eStat.BLL.MLLogic.Models
{
    public class PricePredictionModel
    {
        public Guid UserProductID { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductCharacteristics { get; set; }
        public decimal BaseProductPrice { get; set; }
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
