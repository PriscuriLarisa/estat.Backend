using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IPurchaseProducts
    {
        List<PurchaseProduct> GetAll();
        PurchaseProduct? GetByUid(Guid uid);
        PurchaseProduct Add(PurchaseProduct assignment);
        void Update(PurchaseProduct assignment);
        void Delete(Guid uid);
        List<PurchaseProduct> GetByPurchase(Guid userUid);
        List<PurchaseProduct> GetByUser(Guid userUid);
    }
}
