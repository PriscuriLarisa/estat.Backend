using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.Common.Enums;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Helpers;
using eStat.Library.Models;
using System;
using System.Security.Cryptography;

namespace eStat.BLL.Implementations
{
    public class ProductsBL : BusinessObject, IProducts
    {
        public ProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Product Add(Product product)
        {
            return ProductConverter.ToDTO(_dalContext.Products.Add(ProductConverter.ToEntity(product)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Products.Delete(uid);
        }

        public List<Product> GetAll()
        {
            return _dalContext.Products.GetAll().Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public Dictionary<int, decimal> GetSellsLastSixMonths(Guid uid)
        {
            List<DAL.Entities.PurchaseProduct> purchaseProducts = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now.Date.AddMonths(-6) <= p.Purchase.Date) && p.UserProduct.ProductGUID == uid)
                .ToList();

            Dictionary<int, decimal> dictionary = new();
            DateTime date = DateTime.Now.Date.AddMonths(-6);
            for (int j = 0; j < 6; j++)
            {
                int i = date.Month;
                decimal sumOfPurchases = 0;

                List<DAL.Entities.PurchaseProduct> pcs = purchaseProducts.Where(pc => pc.Purchase.Date.Month == i).ToList();
                foreach (DAL.Entities.PurchaseProduct pc in pcs)
                {
                    sumOfPurchases += pc.Quantity;
                }

                dictionary.Add(i, sumOfPurchases);
                date = date.AddMonths(1);
            }
            return dictionary;
        }

        public Dictionary<int, decimal> GetAvgPriceLastSixMonths(Guid uid)
        {
            List<UserProduct> userProducts = _dalContext.UserProducts.GetAll().Where(up => up.ProductGUID == uid).Select(p => UserProductConverter.ToDTO(p)).ToList();
            List<PriceChange> priceChanges = _dalContext.PriceChanges.GetAll().Where(pc => pc.ProductGUID == uid && (DateTime.Now.Date.AddMonths(-6) <= pc.Date))
                .Select(p => PriceChangeConverter.ToDTO(p))
                .ToList();

            Dictionary<int, decimal> dictionary = new();
            DateTime date = DateTime.Now.Date.AddMonths(-6);
            for (int j = 0; j < 6; j++)
            {
                int i = date.Month;
                decimal sumOfPrices = 0;
                foreach (UserProduct userProduct in userProducts)
                {
                    List<PriceChange> pcs = priceChanges.Where(pc => pc.Date.Month == i && pc.UserProductGUID == userProduct.UserProductGUID).ToList();
                    if (pcs.Count == 0)
                    {
                        List<PriceChange> pcsForUserProduct = priceChanges.Where(pc => pc.UserProductGUID == userProduct.UserProductGUID && pc.Date > date)
                            .OrderByDescending(pc => pc.Date)
                            .ToList();
                        if (pcsForUserProduct.Count == 0)
                        {
                            sumOfPrices += (decimal)userProduct.Price;
                        }
                        else
                        {
                            sumOfPrices += (decimal)pcsForUserProduct.Last().FromPrice;
                        }

                        continue;
                    }

                    decimal sumPerMonth = 0;
                    int daysInMonth = DateTime.DaysInMonth(pcs.FirstOrDefault().Date.Year, pcs.FirstOrDefault().Date.Month);
                    int lastDayCounted = 0;
                    pcs = pcs.OrderBy(pc => pc.Date).ToList();
                    foreach (PriceChange pc in pcs)
                    {
                        sumPerMonth += (pc.Date.Day - lastDayCounted) * pc.FromPrice;
                        lastDayCounted = pc.Date.Day;
                    }
                    sumPerMonth += (daysInMonth - lastDayCounted) * pcs.Last().ToPrice;

                    sumOfPrices += sumPerMonth / daysInMonth;
                }
                dictionary.Add(i, Math.Round(sumOfPrices / userProducts.Count, 2));
                date = date.AddMonths(1);
            }

            return dictionary;
        }

        public Dictionary<int, decimal> GetHighestPriceLastSixMonths(Guid uid)
        {
            List<UserProduct> userProducts = _dalContext.UserProducts.GetAll().Where(up => up.ProductGUID == uid).Select(p => UserProductConverter.ToDTO(p)).ToList();
            List<PriceChange> priceChanges = _dalContext.PriceChanges.GetAll().Where(pc => pc.ProductGUID == uid && (DateTime.Now.Date.AddMonths(-6) <= pc.Date))
                .Select(p => PriceChangeConverter.ToDTO(p))
                .ToList();

            Dictionary<int, decimal> dictionary = new();
            DateTime date = DateTime.Now.Date.AddMonths(-6);
            for (int j = 0; j < 6; j++)
            {
                decimal highestPrice = 0;
                int i = date.Month;
                foreach (UserProduct userProduct in userProducts)
                {
                    List<PriceChange> pcs = priceChanges.Where(pc => pc.Date.Month == i && pc.UserProductGUID == userProduct.UserProductGUID).ToList();
                    if (pcs.Count == 0)
                    {
                        List<PriceChange> pcsForUserProduct = priceChanges.Where(pc => pc.UserProductGUID == userProduct.UserProductGUID && pc.Date > date)
                            .OrderByDescending(pc => pc.Date)
                            .ToList();
                        if (pcsForUserProduct.Count == 0)
                        {
                            highestPrice = Math.Max(highestPrice, (decimal)userProduct.Price);
                        }
                        else
                        {
                            highestPrice = Math.Max(pcsForUserProduct.Last().FromPrice, pcsForUserProduct.Last().ToPrice);
                        }
                    }
                    else
                    {
                        foreach (PriceChange pc in pcs)
                        {
                            highestPrice = Math.Max(highestPrice, pc.FromPrice);
                            highestPrice = Math.Max(highestPrice, pc.ToPrice);
                        }
                    }

                }
                dictionary.Add(i, highestPrice);
                date = date.AddMonths(1);
            }

            return dictionary;
        }

        public Dictionary<int, decimal> GetLowestPriceLastSixMonths(Guid uid)
        {
            List<UserProduct> userProducts = _dalContext.UserProducts.GetAll().Where(up => up.ProductGUID == uid).Select(p => UserProductConverter.ToDTO(p)).ToList();
            List<PriceChange> priceChanges = _dalContext.PriceChanges.GetAll().Where(pc => pc.ProductGUID == uid && (DateTime.Now.Date.AddMonths(-6) <= pc.Date))
                .Select(p => PriceChangeConverter.ToDTO(p))
                .ToList();

            Dictionary<int, decimal> dictionary = new();
            DateTime date = DateTime.Now.Date.AddMonths(-6);
            for (int j = 0; j < 6; j++)
            {
                decimal highestPrice = decimal.MaxValue;
                int i = date.Month;
                foreach (UserProduct userProduct in userProducts)
                {
                    List<PriceChange> pcs = priceChanges.Where(pc => pc.Date.Month == i && pc.UserProductGUID == userProduct.UserProductGUID).ToList();
                    if (pcs.Count == 0)
                    {
                        List<PriceChange> pcsForUserProduct = priceChanges.Where(pc => pc.UserProductGUID == userProduct.UserProductGUID && pc.Date > date)
                            .OrderByDescending(pc => pc.Date)
                            .ToList();
                        if (pcsForUserProduct.Count == 0)
                        {
                            highestPrice = Math.Min(highestPrice, (decimal)userProduct.Price);
                        }
                        else
                        {
                            highestPrice = Math.Min(pcsForUserProduct.Last().FromPrice, pcsForUserProduct.Last().ToPrice);
                        }
                    }
                    else
                    {
                        foreach (PriceChange pc in pcs)
                        {
                            highestPrice = Math.Min(highestPrice, pc.FromPrice);
                            highestPrice = Math.Min(highestPrice, pc.ToPrice);
                        }
                    }
                }
                dictionary.Add(i, highestPrice);
                date = date.AddMonths(1);
            }

            return dictionary;
        }

        public decimal GetAveragePrice(Guid productUid)
        {
            List<UserProduct> userProducts = _dalContext.UserProducts.GetAll().Where(up => up.ProductGUID == productUid).Select(p => UserProductConverter.ToDTO(p)).ToList();
            decimal price = 0;
            foreach (UserProduct userProduct in userProducts)
            {
                price += (decimal)userProduct.Price;
            }
            return Math.Round(price / userProducts.Count, 2);
        }

        public decimal GetAveragePriceLastMonth(Guid uid)
        {
            List<UserProduct> userProducts = _dalContext.UserProducts.GetAll().Where(up => up.ProductGUID == uid).Select(p => UserProductConverter.ToDTO(p)).ToList();
            List<PriceChange> priceChanges = _dalContext.PriceChanges.GetAll().Where(pc => pc.ProductGUID == uid && (DateTime.Now.Date.AddMonths(-1).Month == pc.Date.Month))
                .Select(p => PriceChangeConverter.ToDTO(p))
                .ToList();

            Dictionary<int, decimal> dictionary = new();
            DateTime date = DateTime.Now.Date.AddMonths(-1);
            int i = date.Month;
            decimal price = 0;
            foreach (UserProduct userProduct in userProducts)
            {
                List<PriceChange> pcs = priceChanges.Where(pc => pc.Date.Month == i && pc.UserProductGUID == userProduct.UserProductGUID).ToList();
                if (pcs.Count == 0)
                {
                    List<PriceChange> pcsForUserProduct = priceChanges.Where(pc => pc.UserProductGUID == userProduct.UserProductGUID && pc.Date > date)
                        .OrderByDescending(pc => pc.Date)
                        .ToList();
                    if (pcsForUserProduct.Count == 0)
                    {
                        price = (decimal)userProduct.Price;
                    }
                    else
                    {
                        price = pcsForUserProduct.Last().FromPrice;
                    }
                }
                else
                {
                    decimal sumPerMonth = 0;
                    int daysInMonth = DateTime.DaysInMonth(pcs.FirstOrDefault().Date.Year, pcs.FirstOrDefault().Date.Month);
                    int lastDayCounted = 0;
                    pcs = pcs.OrderBy(pc => pc.Date).ToList();
                    foreach (PriceChange pc in pcs)
                    {
                        sumPerMonth += (pc.Date.Day - lastDayCounted) * pc.FromPrice;
                        lastDayCounted = pc.Date.Day;
                    }
                    sumPerMonth += (daysInMonth - lastDayCounted) * pcs.Last().ToPrice;

                    price += sumPerMonth / daysInMonth;
                }
            }

            return Math.Round(price / userProducts.Count, 2);
        }

        public int GetSellsForLowestPrice(Guid productUid)
        {
            decimal lowestPrice = GetLowestPriceLastSixMonths(productUid)[DateTime.Now.AddMonths(-1).Month];
            List<PurchaseProduct> purchaseProductList = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now - p.Purchase.Date).TotalDays <= 30 && p.UserProduct.ProductGUID == productUid)
                .Select(p => PurchaseProductConverter.ToDTO(p))
            .ToList();
            decimal lowestPriceInterval = lowestPrice + (int)Math.Round(5.0 * Decimal.ToDouble(lowestPrice) / 100.0);
            int nbOfSells = 0;
            foreach (PurchaseProduct pc in purchaseProductList)
            {
                if ((decimal)pc.Price >= lowestPrice && (decimal)pc.Price <= lowestPriceInterval)
                    nbOfSells += pc.Quantity;
            }
            return nbOfSells;
        }

        public int GetSellsForHighestPrice(Guid productUid)
        {
            decimal highest = GetHighestPriceLastSixMonths(productUid)[DateTime.Now.AddMonths(-1).Month];
            List<PurchaseProduct> purchaseProductList = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now - p.Purchase.Date).TotalDays <= 30 && p.UserProduct.ProductGUID == productUid)
                .Select(p => PurchaseProductConverter.ToDTO(p))
            .ToList();
            decimal highestPriceInterval = highest - (int)Math.Round(5.0 * Decimal.ToDouble(highest) / 100.0);
            int nbOfSells = 0;
            foreach (PurchaseProduct pc in purchaseProductList)
            {
                if ((decimal)pc.Price >= highestPriceInterval && (decimal)pc.Price <= highest)
                    nbOfSells += pc.Quantity;
            }
            return nbOfSells;
        }

        public int GetSellsForAveragePrice(Guid productUid)
        {
            decimal average = GetAveragePriceLastMonth(productUid);
            decimal upperPriceInterval = average + (int)Math.Round(5.0 * Decimal.ToDouble(average) / 100.0);
            decimal lowerPriceInterval = average - (int)Math.Round(5.0 * Decimal.ToDouble(average) / 100.0);

            List<PurchaseProduct> purchaseProductList = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now -  p.Purchase.Date).TotalDays <= 30 && p.UserProduct.ProductGUID == productUid)
                .Select(p => PurchaseProductConverter.ToDTO(p))
            .ToList();
            int nbOfSells = 0;
            foreach (PurchaseProduct pc in purchaseProductList)
            {
                if ((decimal)pc.Price >= lowerPriceInterval && (decimal)pc.Price <= upperPriceInterval)
                    nbOfSells += pc.Quantity;
            }
            return nbOfSells;
        }

        public Product? GetByUid(Guid uid)
        {
            DAL.Entities.Product result = _dalContext.Products.GetByUid(uid);
            if (result == null) return null;
            return ProductConverter.ToDTO(result);
        }

        public int GetNumberOfProductsBySearch(List<string> keyWords)
        {
            List<DAL.Entities.Product> allProducts = _dalContext.Products.GetAll();
            if (keyWords != null && keyWords.Count > 0)
            {
                allProducts = allProducts.Where(p => keyWords.Any(kw => p.Characteristics.Contains(kw) || p.Name.Contains(kw))).ToList();
            }
            return allProducts.Count;
        }

        public List<string> GetProductCategories()
        {
            return _dalContext.Products.GetProductsCategories();
        }

        public List<Product> GetProductsByLastPage(int itemsPerPage)
        {
            List<DAL.Entities.Product> products = _dalContext.Products.GetAll();
            int nbofPages = decimal.ToInt32(decimal.Floor(products.Count / itemsPerPage));
            return products.Skip(nbofPages * itemsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public List<Product> GetProductsByPage(int pageNumber, int productsPerPage, string category, SortingCriteria sortingCriteria = SortingCriteria.AlphabeticAscending)
        {
            List<DAL.Entities.Product> allProducts = _dalContext.Products.GetAll();
            if (category != null)
            {
                allProducts = allProducts.Where(p => p.Category == category).ToList();
            }
            List<DAL.Entities.Product> products = SortingHelper.GetSortedProducts(allProducts, sortingCriteria);

            return products.Skip(pageNumber * productsPerPage).Take(productsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public List<Product> GetSearchedProductsByPage(int pageNumber, int productsPerPage, List<string> keyWords, SortingCriteria sortingCriteria)
        {
            List<DAL.Entities.Product> allProducts = _dalContext.Products.GetAll();
            List<DAL.Entities.Product> products = new();
            if (keyWords != null && keyWords.Count > 0)
            {
                products = SortingHelper.GetSortedProducts(allProducts.Where(p => keyWords.Any(kw => p.Name.Contains(kw))).ToList(), sortingCriteria);
                allProducts = SortingHelper.GetSortedProducts(products.Concat(allProducts.Where(p => keyWords.Any(kw => p.Characteristics.Contains(kw) &&
                                                                                                    !products.Any(prd => prd.ProductGUID == p.ProductGUID)))
                                                                                        .ToList())
                    .ToList(), sortingCriteria);
            }
            //List<DAL.Entities.Product> products = SortingHelper.GetSortedProducts(allProducts, sortingCriteria);
            var x = allProducts.Skip((pageNumber - 1) * productsPerPage).Take(productsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
            return allProducts.Skip((pageNumber - 1) * productsPerPage).Take(productsPerPage).Select(p => ProductConverter.ToDTO(p)).ToList();
        }

        public void Update(Product product)
        {
            _dalContext.Products.Update(ProductConverter.ToEntity(product));
        }
    }
}
