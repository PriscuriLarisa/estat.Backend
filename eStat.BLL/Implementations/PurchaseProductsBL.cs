using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    internal class PurchaseProductsBL : BusinessObject, IPurchaseProducts
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
    }
}
