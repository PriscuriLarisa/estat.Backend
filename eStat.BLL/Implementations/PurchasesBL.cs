using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class PurchasesBL : BusinessObject, IPurchases
    {
        public PurchasesBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Purchase Add(Purchase purchase)
        {
            return PurchaseConverter.ToDTO(_dalContext.Purchases.Add(PurchaseConverter.ToEntity(purchase)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Purchases.Delete(uid);
        }

        public List<Purchase> GetAll()
        {
            return _dalContext.Purchases.GetAll().Select(o => PurchaseConverter.ToDTO(o)).ToList();
        }

        public Purchase? GetByUid(Guid uid)
        {
            return PurchaseConverter.ToDTO(_dalContext.Purchases.GetByUid(uid));
        }

        public List<Purchase> GetByUser(Guid userUid)
        {
            return _dalContext.Purchases.GetPurchasesByUser(userUid).Select(o => PurchaseConverter.ToDTO(o)).ToList();
        }

        public void Update(Purchase purchase)
        {
            _dalContext.Purchases.Update(PurchaseConverter.ToEntity(purchase)); ;
        }
    }
}
