using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class PurchaseProductsBL : BusinessObject, IPurchaseProducts
    {
        public PurchaseProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public PurchaseProduct Add(PurchaseProduct purchaseProduct)
        {
            return PurchaseProductConverter.ToDTO(_dalContext.PurchaseProducts.Add(PurchaseProductConverter.ToEntity(purchaseProduct)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.PurchaseProducts.Delete(uid);
        }

        public List<PurchaseProduct> GetAll()
        {
            return _dalContext.PurchaseProducts.GetAll().Select(p => PurchaseProductConverter.ToDTO(p)).ToList();
        }

        public PurchaseProduct? GetByUid(Guid uid)
        {
            return PurchaseProductConverter.ToDTO(_dalContext.PurchaseProducts.GetByUid(uid));
        }

        public List<PurchaseProduct> GetByPurchase(Guid userUid)
        {
            return _dalContext.PurchaseProducts.GetByPurchase(userUid).Select(p => PurchaseProductConverter.ToDTO(p)).ToList();
        }

        public void Update(PurchaseProduct purchaseProduct)
        {
            _dalContext.PurchaseProducts.Update(PurchaseProductConverter.ToEntity(purchaseProduct));
        }

        public List<PurchaseProduct> GetByUser(Guid userUid)
        {
            return _dalContext.PurchaseProducts.GetByUser(userUid).Select(p => PurchaseProductConverter.ToDTO(p)).ToList();
        }

        public List<PurchaseProduct> GetByProductLastMonth(Guid productGuid)
        {
            return _dalContext.PurchaseProducts.GetAll()
                .Where(p => p.UserProduct.ProductGUID == productGuid && 
                            (DateTime.Now - p.Purchase.Date).TotalDays <= 30)
                .Select(p => PurchaseProductConverter.ToDTO(p))
                .ToList();
        }

        public List<PurchaseProduct> GetByUserProductLastMonth( Guid userProductUid)
        {
            return _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now - p.Purchase.Date).TotalDays <= 30 &&
                            p.UserProductGUID == userProductUid)
                .Select(p => PurchaseProductConverter.ToDTO(p))
                .ToList();
        }

        public Dictionary<Guid, int> GetByUserProductsLastMonth(List<Guid> userProductUids)
        {
            List<PurchaseProduct> purchaseProducts = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now - p.Purchase.Date).TotalDays <= 30)
                .Select(p => PurchaseProductConverter.ToDTO(p))
                .ToList();

            Dictionary<Guid, int> purchasesLastMonth = new();
            foreach (PurchaseProduct purchaseProduct in purchaseProducts)
            {
                if (!userProductUids.Contains(purchaseProduct.UserProductGUID))
                    continue;
                if (purchasesLastMonth.ContainsKey(purchaseProduct.UserProductGUID))
                {
                    purchasesLastMonth[purchaseProduct.UserProductGUID] += purchaseProduct.Quantity;
                    continue;
                }

                purchasesLastMonth.Add(purchaseProduct.UserProductGUID, purchaseProduct.Quantity);
            }
            return purchasesLastMonth;
        }

        public Dictionary<Guid, int> GetByProductsLastMonth(List<Guid> productGuids)
        {
            List<PurchaseProduct> purchaseProducts = _dalContext.PurchaseProducts.GetAll()
                .Where(p => (DateTime.Now - p.Purchase.Date).TotalDays <= 30)
                .Select(p => PurchaseProductConverter.ToDTO(p))
                .ToList();

            Dictionary<Guid, int> purchasesLastMonth = new();
            foreach (PurchaseProduct purchaseProduct in purchaseProducts)
            {
                if (purchaseProduct.Product == null || purchaseProduct.Product.ProductGUID == null)
                    continue;

                Guid productGuid = purchaseProduct.Product.ProductGUID;
                if (!productGuids.Contains(productGuid))
                    continue;
                if (purchasesLastMonth.ContainsKey(productGuid))
                {
                    purchasesLastMonth[productGuid] += purchaseProduct.Quantity;
                    continue;
                }

                purchasesLastMonth.Add(productGuid, purchaseProduct.Quantity);
            }
            return purchasesLastMonth;
        }
    }
}
