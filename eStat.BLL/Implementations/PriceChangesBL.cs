using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.DAL.Implementations;
using eStat.Library.Converters;
using eStat.Library.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;

namespace eStat.BLL.Implementations
{
    public class PriceChangesBL : BusinessObject, IPriceChanges
    {
        public PriceChangesBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public PriceChange Add(PriceChangeCreate pc)
        {
            return PriceChangeConverter.ToDTO(_dalContext.PriceChanges.Add(PriceChangeConverter.ToEntity(pc)));
        }

        public List<PriceChange> GetAll()
        {
            return _dalContext.PriceChanges.GetAll().Select(pc => PriceChangeConverter.ToDTO(pc)).ToList();
        }

        public decimal GetAveragePriceLastMonthByUserProduct(Guid userProductGuid)
        {
            List<PriceChange> priceChangesByProduct = _dalContext.PriceChanges.GetAll()
                .Where(pc => pc.UserProductGUID == userProductGuid)
                .OrderByDescending(pc => pc.Date)
                .Select(pc => PriceChangeConverter.ToDTO(pc))
                .ToList();

            if (priceChangesByProduct.Count <= 0)
                return (decimal)_dalContext.UserProducts.GetByUid(userProductGuid).Price;
            return GetAveragePriceLastMonthFromList(priceChangesByProduct);
        }

        public Dictionary<Guid, decimal> GetAveragePricesLastMonthByUserProducts(List<Guid> userProductGuids)
        {
            Dictionary<Guid, List<PriceChange>> priceChangesDictionary = new();
            List<DAL.Entities.PriceChange> priceChanges = _dalContext.PriceChanges.GetAll();
            foreach (DAL.Entities.PriceChange pc in priceChanges)
            {
                if (!userProductGuids.Contains(pc.UserProductGUID))
                    continue;

                if (priceChangesDictionary.ContainsKey(pc.UserProductGUID))
                {
                    priceChangesDictionary[pc.UserProductGUID].Add(PriceChangeConverter.ToDTO(pc));
                    priceChangesDictionary[pc.UserProductGUID] = priceChangesDictionary[pc.UserProductGUID]
                    .OrderByDescending(pc => pc.Date)
                    .ToList();
                    continue;
                }
                else
                {
                    priceChangesDictionary.Add(pc.UserProductGUID, new List<PriceChange> { PriceChangeConverter.ToDTO(pc) });
                }

            }

            Dictionary<Guid, decimal> prices = new();
            foreach (KeyValuePair<Guid, List<PriceChange>> kvp in priceChangesDictionary)
            {
                decimal price = 0;
                if (kvp.Value.Count <= 0)
                    price = (decimal)_dalContext.UserProducts.GetByUid(kvp.Key).Price;
                else
                    price = GetAveragePriceLastMonthFromList(kvp.Value);
                prices.Add(kvp.Key, price);
            }
            return prices;
        }

        public Dictionary<Guid, decimal> GetAveragePricesLastMonthByProducts(List<Guid> productGuids)
        {
            Dictionary<Guid, List<PriceChange>> priceChangesDictionary = new();
            List<DAL.Entities.PriceChange> priceChanges = _dalContext.PriceChanges.GetAll();
            foreach (DAL.Entities.PriceChange pc in priceChanges)
            {
                if (!productGuids.Contains(pc.Product.ProductGUID))
                    continue;

                if (priceChangesDictionary.ContainsKey(pc.Product.ProductGUID))
                {
                    priceChangesDictionary[pc.Product.ProductGUID].Add(PriceChangeConverter.ToDTO(pc));
                    priceChangesDictionary[pc.Product.ProductGUID] = priceChangesDictionary[pc.Product.ProductGUID]
                    .OrderByDescending(pc => pc.Date)
                    .ToList();
                    continue;
                }
                else
                {
                    priceChangesDictionary.Add(pc.Product.ProductGUID, new List<PriceChange> { PriceChangeConverter.ToDTO(pc) });
                }

            }

            Dictionary<Guid, decimal> prices = new();
            foreach (KeyValuePair<Guid, List<PriceChange>> kvp in priceChangesDictionary)
            {
                decimal price = 0;
                if (kvp.Value.Count <= 0)
                {
                    List<DAL.Entities.UserProduct> ups = _dalContext.UserProducts.GetUserProductsByProduct(kvp.Key);
                    decimal sumOfPrices = (decimal)ups.Sum(i => i.Price);
                    if (ups.Count == 0)
                        price = 0;
                    else
                        price = Math.Round(sumOfPrices / ups.Count());
                }
                else
                    price = GetAveragePriceLastMonthFromList(kvp.Value);
                prices.Add(kvp.Key, Math.Round(price, 2));
            }
            return prices;
        }

        private decimal GetAveragePriceLastMonthFromList(List<PriceChange> priceChangesByProduct)
        {
            int nbOfDays = 30;

            Dictionary<Guid, List<PriceChange>> dictionary = new();

            foreach (PriceChange pc in priceChangesByProduct)
            {
                if (dictionary.ContainsKey(pc.UserProductGUID))
                {
                    dictionary[pc.UserProductGUID].Add(pc);
                    dictionary[pc.UserProductGUID] = dictionary[pc.UserProductGUID]
                    .OrderByDescending(pc => pc.Date)
                    .ToList();
                    continue;
                }
                else
                {
                    dictionary.Add(pc.UserProductGUID, new List<PriceChange> { pc });
                }
            }

            int nbOfUserProducts = priceChangesByProduct.Count;
            if (nbOfUserProducts == 0)
                return 0;

            decimal sumOfPrices = 0;
            foreach (KeyValuePair<Guid, List<PriceChange>> kvp in dictionary)
            {
                List<PriceChange> priceChangesByUserProduct = kvp.Value;
                if ((DateTime.Now - priceChangesByUserProduct.First().Date).TotalDays >= nbOfDays)
                    return priceChangesByUserProduct.First().ToPrice;

                int daysDifference = (int)(DateTime.Now - priceChangesByUserProduct.First().Date).TotalDays;
                decimal sumOfPricesByUP = priceChangesByUserProduct.First().ToPrice * daysDifference;
                nbOfDays -= daysDifference;
                DateTime nextPriceChange = priceChangesByUserProduct.First().Date;
                List<PriceChange> newPriceChangesByProduct = priceChangesByUserProduct.Skip(1).ToList();

                foreach (PriceChange priceChange in newPriceChangesByProduct)
                {
                    if (nbOfDays == 0)
                        break;

                    DateTime previousPriceChange = priceChange.Date;
                    daysDifference = (int)(nextPriceChange - previousPriceChange).TotalDays;

                    if (daysDifference >= nbOfDays)
                        nbOfDays = 0;

                    sumOfPricesByUP += priceChange.ToPrice * daysDifference;
                    nbOfDays -= daysDifference;
                    nextPriceChange = previousPriceChange;
                }

                if (nbOfDays > 0)
                {
                    PriceChange priceChange = priceChangesByUserProduct.Last();
                    sumOfPricesByUP += priceChange.FromPrice * nbOfDays;
                }
                sumOfPrices += sumOfPricesByUP / 30;
            }

            return sumOfPrices / nbOfUserProducts;
        }

        public List<PriceChange> GetByProduct(Guid productGuid)
        {
            return _dalContext.PriceChanges.GetAll()
                .Where(pc => pc.ProductGUID == productGuid)
                .Select(pc => PriceChangeConverter.ToDTO(pc))
                .ToList();
        }

        public List<PriceChange> GetByProductInLastMonth(Guid productGuid)
        {
            return _dalContext.PriceChanges.GetAll()
                .Where(pc => pc.ProductGUID == productGuid &&
                                (DateTime.Now - pc.Date).TotalDays <= 30)
                .Select(pc => PriceChangeConverter.ToDTO(pc))
                .ToList();
        }

        public PriceChange? GetByUid(Guid uid)
        {
            return PriceChangeConverter.ToDTO(_dalContext.PriceChanges.GetByUid(uid));
        }

        public List<PriceChange> GetByUser(Guid userGuid, Guid productGuid)
        {
            return _dalContext.PriceChanges.GetAll()
                .Where(pc => pc.ProductGUID == productGuid &&
                                pc.UserProduct != null &&
                                pc.UserProduct.UserGUID == userGuid)
                .Select(pc => PriceChangeConverter.ToDTO(pc))
                .ToList();
        }

        public List<PriceChange> GetByUserInLastMonth(Guid userGuid, Guid productGuid)
        {
            return _dalContext.PriceChanges.GetAll()
                .Where(pc => pc.ProductGUID == productGuid &&
                                pc.UserProduct != null &&
                                pc.UserProduct.UserGUID == userGuid &&
                                (DateTime.Now - pc.Date).TotalDays <= 30)
                .Select(pc => PriceChangeConverter.ToDTO(pc))
                .ToList();
        }

        public List<PriceChange> GetFromLastWeek()
        {
            return _dalContext.PriceChanges.GetAll()
                .Where(pc => (DateTime.Now - pc.Date).TotalDays <= 7)
                .Select(pc => PriceChangeConverter.ToDTO(pc))
                .ToList();
        }

        public void Update(PriceChange pc)
        {
            _dalContext.PriceChanges.Update(PriceChangeConverter.ToEntity(pc));
        }

        public PriceChange AddWithDate(PriceChangeCreate pc, DateTime date)
        {
            DAL.Entities.PriceChange priceChange = new DAL.Entities.PriceChange
            {
                PriceChangeGUID = Guid.Empty,
                Date = date,
                UserProduct = null,
                UserProductGUID = pc.UserProductGUID,
                ProductGUID = pc.ProductGUID,
                Product = null,
                FromPrice = pc.FromPrice,
                ToPrice = pc.ToPrice,
            };

            return PriceChangeConverter.ToDTO(_dalContext.PriceChanges.Add(priceChange));

        }
    }
}