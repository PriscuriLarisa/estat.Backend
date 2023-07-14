using CsvHelper;
using eStat.BLL.Interfaces;
using eStat.BLL.MLLogic.Interfaces;
using eStat.BLL.MLLogic.MLHelpers;
using eStat.BLL.MLLogic.Models;
using eStat.DAL.Implementations;
using eStat.Library.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace eStat.BLL.MLLogic
{
    public class PredictionDataGenerator : IPredicitonDataGenerator
    {

        private readonly IPriceChanges _priceChangesService;
        private readonly ISearches _searchesService;
        private readonly IUserProducts _userProductsService;
        private readonly IPurchaseProducts _purchaseProductsService;
        private readonly string _trainDatasetFilePath = "..\\eStat.BLL\\MLLogic\\SolutionItems\\train.csv";
        private readonly string _predictDatasetFilePath = "..\\eStat.BLL\\MLLogic\\SolutionItems\\test.csv";
        private Dictionary<Guid, ProductStatistics> _productStatistics;

        public PredictionDataGenerator(IServiceScopeFactory factory)
        {
            _priceChangesService = factory.CreateScope().ServiceProvider.GetRequiredService<IPriceChanges>();
            _searchesService = factory.CreateScope().ServiceProvider.GetRequiredService<ISearches>();
            _userProductsService = factory.CreateScope().ServiceProvider.GetRequiredService<IUserProducts>();
            _purchaseProductsService = factory.CreateScope().ServiceProvider.GetRequiredService<IPurchaseProducts>();
        }

        public void GenerateData()
        {
            
            _productStatistics = new();
            List<UserProduct> userProducts = _userProductsService.GetAll();
            List<Search> searches = _searchesService.GetAll();
            List<Guid> productGuidSearched = searches.Select(s => s.ProductGUID).Distinct().ToList();
            List<PurchaseProduct> purchaseProducts = _purchaseProductsService.GetAll();
            List<Guid> productGuidPurchased = purchaseProducts.Select(p => p.UserProductGUID).Distinct().ToList();
            List<Models.PricePredictionModel> pricePredictionModels = new List<Models.PricePredictionModel>();

            Random random = new Random();

            userProducts = userProducts
                .Where(up => productGuidSearched.Contains(up.UserProductGUID) || productGuidPurchased.Contains(up.UserProductGUID))
                .OrderBy(x => random.Next()).Take(1000).ToList();

            Dictionary<Guid, decimal> averagePricesLastMonth = _priceChangesService.GetAveragePricesLastMonthByUserProducts(userProducts.Select(up => up.UserProductGUID).ToList());
            Dictionary<Guid, int> purchasesLastMonth = _purchaseProductsService.GetByUserProductsLastMonth(userProducts.Select(up => up.UserProductGUID).ToList());
            
            List<Guid> productsGuid = userProducts.Select(up => up.ProductGUID).Distinct().ToList();
            GenerateProductsStatistics(productsGuid);

            foreach (UserProduct userProduct in userProducts)
            {
                if (userProduct.Quantity <= 0)
                    continue;

                Guid userProductGuid = userProduct.UserProductGUID;
                Guid productGuid = userProduct.ProductGUID;
                decimal baseProductPrice = userProduct.Product.BasePrice;
                string productName = userProduct.Product.Name;
                string productCharacteristics = userProduct.Product.Characteristics;
                ProductStatistics productStatistics = GetProductStatistics(productGuid);
                int myStock = userProduct.Quantity;
                int mySellsLastMonth = purchasesLastMonth.ContainsKey(userProduct.UserProductGUID) ? purchasesLastMonth[userProductGuid] : 0;
                decimal myAveragePriceLastMonth = averagePricesLastMonth.ContainsKey(userProductGuid) ? averagePricesLastMonth[userProductGuid] : (decimal)userProduct.Price;
                decimal myCurrentPrice = (decimal)userProduct.Price;
                Models.PricePredictionModel pricePrediction = new Models.PricePredictionModel
                {
                    UserProductID = userProductGuid,
                    ProductID = productGuid,
                    ProductName = productName,
                    ProductCharacteristics = productCharacteristics,
                    BaseProductPrice = baseProductPrice,
                    NbOfPurchasesLastMonth = productStatistics.NbOfPurchasesLastMonth,
                    NbOfSearchesLastMonth = productStatistics.NbOfSearchesLastMonth,
                    CurrentAveragePrice = productStatistics.CurrentAveragePrice,
                    CurrentAverageStockPerRetailer = productStatistics.CurrentAverageStockPerRetailer,
                    AveragePriceLastMonth = productStatistics.AveragePriceLastMonth,
                    AverageStockPerRetailerLastMonth = productStatistics.AverageStockPerRetailerLastMonth,
                    MyStock = myStock,
                    MyAveragePriceLastMonth = myAveragePriceLastMonth,
                    MyCurrentPrice = myCurrentPrice,
                    MySellsLastMonth = mySellsLastMonth
                };
                pricePredictionModels.Add(pricePrediction);
            }

            CSVWriter.Write(pricePredictionModels, _predictDatasetFilePath);
        }

        private void GenerateProductsStatistics(List<Guid> productGuids)
        {
            Dictionary<Guid, int> nbOfSearchesLastMonth = _searchesService.GetNbOfSearchesByProductFromLastMonth(productGuids);
            Dictionary<Guid, int> nbOfPurchasesLastMonth = _purchaseProductsService.GetByProductsLastMonth(productGuids);
            Dictionary<Guid, decimal> averagePriceLastMonth = _priceChangesService.GetAveragePricesLastMonthByProducts(productGuids);
            Dictionary<Guid, decimal> currentAveragePrice = ComputeCurrentAveragePriceForProducts(productGuids);
            Dictionary<Guid, int> currentAverageStockPerRetailer = ComputeCurrentAverageStockForProducts(productGuids);
            Dictionary<Guid, int> averageStockPerRetailerLastMonth = ComputeAverageStockForProductLastMonth(productGuids);

            foreach(Guid productGuid in productGuids)
            {
                ProductStatistics productStatistics = new ProductStatistics
                {
                    NbOfPurchasesLastMonth = nbOfPurchasesLastMonth.ContainsKey(productGuid) ? nbOfPurchasesLastMonth[productGuid] : 0,
                    NbOfSearchesLastMonth = nbOfSearchesLastMonth.ContainsKey(productGuid) ? nbOfSearchesLastMonth[productGuid] : 0,
                    AveragePriceLastMonth = averagePriceLastMonth.ContainsKey(productGuid) ? averagePriceLastMonth[productGuid] : currentAveragePrice.ContainsKey(productGuid) ? currentAveragePrice[productGuid] : 0,
                    CurrentAveragePrice = currentAveragePrice.ContainsKey(productGuid) ? currentAveragePrice[productGuid] : 0,
                    CurrentAverageStockPerRetailer = currentAverageStockPerRetailer.ContainsKey(productGuid) ? currentAverageStockPerRetailer[productGuid] : 0,
                    AverageStockPerRetailerLastMonth = averageStockPerRetailerLastMonth.ContainsKey(productGuid) ? averageStockPerRetailerLastMonth[productGuid] : 0,
                };

                _productStatistics.Add(productGuid, productStatistics);
            }

        }

        private ProductStatistics GetProductStatistics(Guid productGuid)
        {
            if (_productStatistics.ContainsKey(productGuid))
                return _productStatistics[productGuid];
            else return new ProductStatistics
            {
                NbOfPurchasesLastMonth = 0,
                NbOfSearchesLastMonth = 0,
                AveragePriceLastMonth = 0,
                CurrentAveragePrice = 0,
                CurrentAverageStockPerRetailer = 0,
                AverageStockPerRetailerLastMonth = 0
            };


            //int nbOfSearchesLastMonth = _searchesService.GetByProductFromLastMonth(productGuid).Count;
            //int nbOfPurchasesLastMonth = _purchaseProductsService.GetByProductLastMonth(productGuid).Count;
            //decimal averagePriceLastMonth = Math.Round(ComputeAveragePriceLastMonth(productGuid));
            //decimal currentAveragePrice = Math.Round(ComputeCurrentAveragePriceForProduct(productGuid), 2);
            //int currentAverageStockPerRetailer = ComputeCurrentAverageStockForProduct(productGuid);
            //int averageStockPerRetailerLastMonth = ComputeAverageStockForProductLastMonth(productGuid);
            //ProductStatistics productStatistics = new ProductStatistics
            //{
            //    NbOfPurchasesLastMonth = nbOfPurchasesLastMonth,
            //    NbOfSearchesLastMonth = nbOfSearchesLastMonth,
            //    AveragePriceLastMonth = averagePriceLastMonth,
            //    CurrentAveragePrice = currentAveragePrice,
            //    CurrentAverageStockPerRetailer = currentAverageStockPerRetailer,
            //    AverageStockPerRetailerLastMonth = averageStockPerRetailerLastMonth
            //};

            //_productStatistics.Add(productGuid, productStatistics);
            //return productStatistics;
        }

        private decimal ComputeCurrentAveragePriceForProduct(Guid productGuid)
        {
            List<UserProduct> userProducts = _userProductsService.GetUserProductsByProduct(productGuid);
            decimal sumOfValues = 0;
            foreach (UserProduct userProduct in userProducts)
            {
                sumOfValues += (decimal)userProduct.Price;
            }
            if (userProducts.Count == 0)
                return 0;
            return sumOfValues / userProducts.Count;
        }

        private Dictionary<Guid, decimal> ComputeCurrentAveragePriceForProducts(List<Guid> productGuids)
        {
            Dictionary<Guid, decimal> pricesDictionary = new();
            List<UserProduct> allUserProducts = _userProductsService.GetAll();
            Dictionary<Guid, List<UserProduct>> userProductsByProduct = new();
            foreach (UserProduct userProduct in allUserProducts)
            {
                if (!productGuids.Contains(userProduct.ProductGUID))
                    continue;
                if (userProductsByProduct.ContainsKey(userProduct.ProductGUID))
                {
                    userProductsByProduct[userProduct.ProductGUID].Add(userProduct);
                    continue;
                }
                userProductsByProduct.Add(userProduct.ProductGUID, new List<UserProduct> { userProduct });
            }

            Dictionary<Guid, decimal> productsPrices = new();
            foreach (KeyValuePair<Guid, List<UserProduct>> kvp in userProductsByProduct)
            {
                decimal sumOfValues = 0;
                foreach (UserProduct userProduct in kvp.Value)
                {
                    sumOfValues += (decimal)userProduct.Price;
                }
                if(kvp.Value.Count == 0) {
                    productsPrices.Add(kvp.Key, 0);
                    continue;
                }
                productsPrices.Add(kvp.Key, Math.Round(sumOfValues / kvp.Value.Count,2));
            }

            return productsPrices;
        }

        private int ComputeCurrentAverageStockForProduct(Guid productGuid)
        {
            List<UserProduct> userProducts = _userProductsService.GetUserProductsByProduct(productGuid);
            int sumOfValues = 0;
            foreach (UserProduct userProduct in userProducts)
            {
                sumOfValues += userProduct.Quantity;
            }
            if (userProducts.Count == 0)
                return 0;
            return (int)sumOfValues / userProducts.Count;
        }

        private Dictionary<Guid, int> ComputeCurrentAverageStockForProducts(List<Guid> productGuids)
        {
            Dictionary<Guid, decimal> pricesDictionary = new();
            List<UserProduct> allUserProducts = _userProductsService.GetAll();
            Dictionary<Guid, List<UserProduct>> userProductsByProduct = new();
            foreach (UserProduct userProduct in allUserProducts)
            {
                if (!productGuids.Contains(userProduct.ProductGUID))
                    continue;
                if (userProductsByProduct.ContainsKey(userProduct.ProductGUID))
                {
                    userProductsByProduct[userProduct.ProductGUID].Add(userProduct);
                    continue;
                }
                userProductsByProduct.Add(userProduct.ProductGUID, new List<UserProduct> { userProduct });
            }

            Dictionary<Guid, int> productsStock = new();
            foreach (KeyValuePair<Guid, List<UserProduct>> kvp in userProductsByProduct)
            {
                int stock = 0;
                foreach (UserProduct userProduct in kvp.Value)
                {
                    stock += userProduct.Quantity;
                }
                if (kvp.Value.Count == 0)
                {
                    productsStock.Add(kvp.Key, 0);
                    continue;
                }
                productsStock.Add(kvp.Key, (int) stock / kvp.Value.Count);
            }

            return productsStock;
        }

        private decimal ComputeAveragePriceLastMonth(Guid productGuid)
        {
            List<UserProduct> userProducts = _userProductsService.GetUserProductsByProduct(productGuid);
            decimal sumOfValues = 0;
            foreach (UserProduct userProduct in userProducts)
            {
                sumOfValues += _priceChangesService.GetAveragePriceLastMonthByUserProduct(userProduct.UserProductGUID);
            }
            if (userProducts.Count == 0)
                return 0;
            return sumOfValues / userProducts.Count;
        }

        private Dictionary<Guid, decimal> ComputeAveragePriceLastMonthForProducts(List<Guid> productGuids)
        {
            Dictionary<Guid, decimal> pricesDictionary = new();
            List<UserProduct> allUserProducts = _userProductsService.GetAll();
            Dictionary<Guid, List<UserProduct>> userProductsByProduct = new();
            foreach (UserProduct userProduct in allUserProducts)
            {
                if (!productGuids.Contains(userProduct.ProductGUID))
                    continue;
                if (userProductsByProduct.ContainsKey(userProduct.ProductGUID))
                {
                    userProductsByProduct[userProduct.ProductGUID].Add(userProduct);
                    continue;
                }
                userProductsByProduct.Add(userProduct.ProductGUID, new List<UserProduct> { userProduct });
            }

            foreach (Guid productGuid in productGuids)
            {
                if (!userProductsByProduct.ContainsKey(productGuid))
                    pricesDictionary.Add(productGuid, 0);
                List<UserProduct> userProducts = userProductsByProduct[productGuid];
                decimal sumOfValues = 0;

                Dictionary<Guid, decimal> dictionary = _priceChangesService.GetAveragePricesLastMonthByUserProducts(userProducts.Select(up => up.UserProductGUID).ToList());

                foreach (KeyValuePair<Guid, decimal> kvp in dictionary)
                {
                    sumOfValues += kvp.Value;
                }

                if(dictionary.Count == 0)
                {
                    pricesDictionary.Add(productGuid, 0);
                    continue;
                }
                if (dictionary.Count == 0)
                {
                    pricesDictionary.Add(productGuid, 0);
                    continue;
                }

                pricesDictionary.Add(productGuid, Math.Round(sumOfValues / dictionary.Count, 2));
            }

            return pricesDictionary;
        }

        private int ComputeMySalesLastMonth(Guid userProductGuid)
        {
            List<PurchaseProduct> purchaseProducts = _purchaseProductsService.GetByUserProductLastMonth(userProductGuid);
            int sales = 0;
            foreach (PurchaseProduct purchaseProduct in purchaseProducts)
            {
                sales += purchaseProduct.Quantity;
            }
            return sales;
        }

        private int ComputeAverageStockForProductLastMonth(Guid productGuid)
        {
            List<UserProduct> userProducts = _userProductsService.GetUserProductsByProduct(productGuid);
            decimal sumOfValues = 0;
            decimal retailerStock = 0;
            foreach (UserProduct userProduct in userProducts)
            {
                retailerStock = userProduct.Quantity;
                List<PurchaseProduct> purchaseProducts = _purchaseProductsService.GetByUserProductLastMonth(userProduct.UserProductGUID);
                foreach (PurchaseProduct purchaseProduct in purchaseProducts)
                {
                    retailerStock += purchaseProduct.Quantity;
                }

                sumOfValues += retailerStock;
            }
            if(userProducts.Count == 0)
            {
                return 0;
            }

            return (int)sumOfValues / userProducts.Count;
        }

        private Dictionary<Guid, int> ComputeAverageStockForProductLastMonth(List<Guid> productGuids)
        {
            Dictionary<Guid, decimal> pricesDictionary = new();
            List<UserProduct> allUserProducts = _userProductsService.GetAll();
            Dictionary<Guid, List<UserProduct>> userProductsByProduct = new();
            List<Guid> userProductGuids = new();
            foreach (UserProduct userProduct in allUserProducts)
            {
                if (!productGuids.Contains(userProduct.ProductGUID))
                    continue;
                userProductGuids.Add(userProduct.UserProductGUID);
                if (userProductsByProduct.ContainsKey(userProduct.ProductGUID))
                {
                    userProductsByProduct[userProduct.ProductGUID].Add(userProduct);
                    continue;
                }
                userProductsByProduct.Add(userProduct.ProductGUID, new List<UserProduct> { userProduct });
            }

            List<PurchaseProduct> allPurchaseProducts = _purchaseProductsService.GetAll();
            Dictionary<Guid, List<PurchaseProduct>> purchasesByproduct = new();
            foreach (PurchaseProduct purchaseProduct in allPurchaseProducts)
            {
                if (!userProductGuids.Contains(purchaseProduct.UserProductGUID))
                    continue;
                if (purchasesByproduct.ContainsKey(purchaseProduct.UserProductGUID))
                {
                    purchasesByproduct[purchaseProduct.UserProductGUID].Add(purchaseProduct);
                    continue;
                }
                purchasesByproduct.Add(purchaseProduct.UserProductGUID, new List<PurchaseProduct> { purchaseProduct });
            }


            foreach (UserProduct userProduct in allUserProducts)
            {
                if (!productGuids.Contains(userProduct.ProductGUID))
                    continue;
                if (userProductsByProduct.ContainsKey(userProduct.ProductGUID))
                {
                    userProductsByProduct[userProduct.ProductGUID].Add(userProduct);
                    continue;
                }
                userProductsByProduct.Add(userProduct.ProductGUID, new List<UserProduct> { userProduct });
            }

            Dictionary<Guid, int> productStockDictionary = new();
            foreach (Guid productGuid in productGuids)
            {

                if (!userProductsByProduct.ContainsKey(productGuid))
                    pricesDictionary.Add(productGuid, 0);

                decimal sumOfValues = 0;
                decimal retailerStock = 0;
                List<UserProduct> userProducts = userProductsByProduct[productGuid];
                foreach (UserProduct userProduct in userProducts)
                {
                    retailerStock = userProduct.Quantity;
                    List<PurchaseProduct> purchaseProducts = purchasesByproduct.ContainsKey(userProduct.UserProductGUID) ? purchasesByproduct[userProduct.UserProductGUID] : new();
                    foreach (PurchaseProduct purchaseProduct in purchaseProducts)
                    {
                        retailerStock += purchaseProduct.Quantity;
                    }

                    sumOfValues += retailerStock;
                }
                if(userProducts.Count == 0)
                {
                    productStockDictionary.Add(productGuid, 0);
                    continue;
                }
                productStockDictionary.Add(productGuid, (int)sumOfValues / userProducts.Count);
            }

            return productStockDictionary;
        }

        public void GenerateInitialDatasetFromDatabase()
        {
            Random random = new Random();
            List<UserProduct> userProducts = _userProductsService.GetAll().OrderBy(x => random.Next()).Take(10000).ToList();
            List<Models.PricePredictionModel> pricePredictionModels = new List<Models.PricePredictionModel>();


            foreach (UserProduct userProduct in userProducts)
            {
                Guid userProductGuid = userProduct.UserProductGUID;
                Guid productGuid = userProduct.ProductGUID;
                decimal baseProductPrice = userProduct.Product.BasePrice;
                string productName = userProduct.Product.Name;
                string productCharacteristics = userProduct.Product.Characteristics;
                int nbOfSearchesLastMonth = random.Next(0, 200);
                int nbOfPurchasesLastMonth = random.Next(0, 20);
                decimal averagePriceLastMonth = userProduct.Product.BasePrice + random.Next((int)Math.Round(-(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));
                decimal currentAveragePrice = averagePriceLastMonth + random.Next((int)Math.Round(-(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));
                int currentAverageStockPerRetailer = random.Next(0, 40);
                int averageStockPerRetailerLastMonth = random.Next(0, 40);
                int myStock = random.Next(1, 40);
                int mySellsLastMonth = random.Next(0, 10);
                decimal myAveragePriceLastMonth = userProduct.Product.BasePrice + random.Next((int)Math.Round(-(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));

                decimal myCurrentPrice = myAveragePriceLastMonth + random.Next((int)Math.Round(-(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(5.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));
                decimal predictedPrice = myAveragePriceLastMonth + random.Next((int)Math.Round(-(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(10.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));

                Models.PricePredictionModel pricePrediction = new Models.PricePredictionModel
                {
                    UserProductID = userProductGuid,
                    ProductID = productGuid,
                    ProductName = productName,
                    ProductCharacteristics = productCharacteristics,
                    BaseProductPrice = baseProductPrice,
                    NbOfPurchasesLastMonth = nbOfPurchasesLastMonth,
                    NbOfSearchesLastMonth = nbOfSearchesLastMonth,
                    CurrentAveragePrice = currentAveragePrice,
                    CurrentAverageStockPerRetailer = currentAverageStockPerRetailer,
                    AveragePriceLastMonth = averagePriceLastMonth,
                    AverageStockPerRetailerLastMonth = averageStockPerRetailerLastMonth,
                    MyStock = myStock,
                    MyAveragePriceLastMonth = myAveragePriceLastMonth,
                    MyCurrentPrice = myCurrentPrice,
                    MySellsLastMonth = mySellsLastMonth,
                    PredictedPrice = predictedPrice,
                };
                pricePredictionModels.Add(pricePrediction);
            }

            CSVWriter.Write(pricePredictionModels, _trainDatasetFilePath);
        }

        public void GenerateInitialDataset()
        {
            List<UserProduct> userProducts = _userProductsService.GetAll();
            List<Models.PricePredictionModel> pricePredictionModels = new List<Models.PricePredictionModel>();

            Random random = new Random();

            foreach (UserProduct userProduct in userProducts)
            {
                Guid userProductGuid = userProduct.UserProductGUID;
                Guid productGuid = userProduct.ProductGUID;
                decimal baseProductPrice = userProduct.Product.BasePrice;
                string productName = userProduct.Product.Name;
                string productCharacteristics = userProduct.Product.Characteristics;
                int nbOfSearchesLastMonth = random.Next(0, 200);
                int nbOfPurchasesLastMonth = random.Next(0, 30);
                decimal averagePriceLastMonth = userProduct.Product.BasePrice + random.Next((int)Math.Round(-(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));
                decimal currentAveragePrice = nbOfPurchasesLastMonth > 15 ?
                                                averagePriceLastMonth + random.Next(0, (int)Math.Round(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0)) :
                                                averagePriceLastMonth + random.Next((int)Math.Round(-(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(5.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));
                int currentAverageStockPerRetailer = random.Next(1, 20);
                int averageStockPerRetailerLastMonth = random.Next(1, 20);
                int myStock = random.Next(1, 40);
                int mySellsLastMonth = random.Next(0, 10);
                decimal myAveragePriceLastMonth = userProduct.Product.BasePrice + random.Next((int)Math.Round(-(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));

                decimal myCurrentPrice = mySellsLastMonth > 15 ?
                                                myAveragePriceLastMonth + random.Next(0, (int)Math.Round(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0)) :
                                                myAveragePriceLastMonth + random.Next((int)Math.Round(-(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(5.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));
                decimal predictedPrice = mySellsLastMonth > 15 && myStock >= currentAverageStockPerRetailer - 5 ?
                                                myAveragePriceLastMonth + random.Next(0, (int)Math.Round(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0)) :
                                                myAveragePriceLastMonth + random.Next((int)Math.Round(-(15.0 * Decimal.ToDouble(userProduct.Product.BasePrice)) / 100.0), (int)Math.Round(5.0 * Decimal.ToDouble(userProduct.Product.BasePrice) / 100.0));

                Models.PricePredictionModel pricePrediction = new Models.PricePredictionModel
                {
                    UserProductID = userProductGuid,
                    ProductID = productGuid,
                    ProductName = productName,
                    ProductCharacteristics = productCharacteristics,
                    BaseProductPrice = baseProductPrice,
                    NbOfPurchasesLastMonth = nbOfPurchasesLastMonth,
                    NbOfSearchesLastMonth = nbOfSearchesLastMonth,
                    CurrentAveragePrice = currentAveragePrice,
                    CurrentAverageStockPerRetailer = currentAverageStockPerRetailer,
                    AveragePriceLastMonth = averagePriceLastMonth,
                    AverageStockPerRetailerLastMonth = averageStockPerRetailerLastMonth,
                    MyStock = myStock,
                    MyAveragePriceLastMonth = myAveragePriceLastMonth,
                    MyCurrentPrice = myCurrentPrice,
                    MySellsLastMonth = mySellsLastMonth,
                    PredictedPrice = predictedPrice,
                };
                pricePredictionModels.Add(pricePrediction);
            }

            CSVWriter.Write(pricePredictionModels.OrderBy(x => random.Next()).Take(10000).ToList(), _trainDatasetFilePath);
        }
    }
}
