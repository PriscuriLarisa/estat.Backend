using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IPurchaseProducts
    {
        List<PurchaseProduct> GetAll();
        PurchaseProduct? GetByUid(Guid uid);
        PurchaseProduct Add(PurchaseProduct purchaseProduct);
        void Update(PurchaseProduct purchaseProduct);
        void Delete(Guid uid);
        List<PurchaseProduct> GetByPurchase(Guid purchaseUid);
        List<PurchaseProduct> GetByUser(Guid userUid);
    }
}