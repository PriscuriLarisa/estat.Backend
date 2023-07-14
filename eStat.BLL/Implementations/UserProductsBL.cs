using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.Common.Enums;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Helpers;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class UserProductsBL : BusinessObject, IUserProducts
    {
        public UserProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public UserProduct Add(UserProduct userProduct)
        {
            if(_dalContext.Products.GetByUid(userProduct.ProductGUID) == null)
            {
                return null;
            }
            if (_dalContext.Users.GetByUid(userProduct.UserGUID) == null)
            {
                return null;
            }
            userProduct.UserProductGUID = Guid.Empty;
            userProduct.Product = null;
            userProduct.User = null;
            return UserProductConverter.ToDTO(_dalContext.UserProducts.Add(UserProductConverter.ToEntity(userProduct)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.UserProducts.Delete(uid);
        }

        public List<UserProduct> GetAll()
        {
            return _dalContext.UserProducts.GetAll().Select(p => UserProductConverter.ToDTO(p)).ToList();
        }

        public UserProduct? GetByUid(Guid uid)
        {
            return UserProductConverter.ToDTO(_dalContext.UserProducts.GetByUid(uid));
        }

        public List<UserProduct> GetUserProductsByProduct(Guid productUid)
        {
            return _dalContext.UserProducts.GetUserProductsByProduct(productUid).Select(p => UserProductConverter.ToDTOWithUser(p)).ToList();
        }

        public List<UserProduct> GetUserProductsByProductInBatches(Guid userUid, int batchNb, List<string> keyWords)
        {
            List<UserProduct> allProducts = GetUserProductsByUser(userUid);
            if (keyWords == null || keyWords.Count() == 0)
                return allProducts.Skip(batchNb * 20).Take(20).ToList();

            allProducts = allProducts.Where(p => keyWords.Any(kw => p.Product.Name.Contains(kw))).ToList();
            allProducts = allProducts.Concat(allProducts.Where(p => keyWords.Any(kw => p.Product.Characteristics.Contains(kw))).ToList()).ToList();

            return allProducts.Skip(batchNb * 20).Take(20).ToList();
        }

        public List<UserProduct> GetUserProductsByUser(Guid userUid)
        {
            return _dalContext.UserProducts.GetUserProductsByUser(userUid).Select(p => UserProductConverter.ToDTO(p)).ToList();
        }

        public void Update(UserProduct userProduct)
        {
            _dalContext.UserProducts.Update(UserProductConverter.ToEntity(userProduct));
        }

        public Dictionary<int, decimal> GetMySellsLastSixMonths(Guid uid)
        {
            List<DAL.Entities.PurchaseProduct> purchaseProducts = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now.Date.AddMonths(-6) <= p.Purchase.Date) && p.UserProductGUID == uid)
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

        public Dictionary<int, decimal> GetAvgPriceLastSixMonthsByUserProduct(Guid uid)
        {
            UserProduct userProduct = UserProductConverter.ToDTO(_dalContext.UserProducts.GetByUid(uid));
            List<PriceChange> priceChanges = _dalContext.PriceChanges.GetAll().Where(pc => pc.UserProductGUID == uid && (DateTime.Now.Date.AddMonths(-6) <= pc.Date))
                .Select(p => PriceChangeConverter.ToDTO(p))
                .ToList();

            Dictionary<int, decimal> dictionary = new();
            DateTime date = DateTime.Now.Date.AddMonths(-6);
            for (int j = 0; j < 6; j++)
            {
                int i = date.Month;
                decimal sumOfPrices = 0;
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

                    sumOfPrices += sumPerMonth / daysInMonth;
                }


                dictionary.Add(i, Math.Round(sumOfPrices, 2));
                date = date.AddMonths(1);
            }
            return dictionary;
        }
    }
}
