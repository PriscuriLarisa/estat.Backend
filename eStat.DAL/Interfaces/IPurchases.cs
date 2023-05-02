using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IPurchases
    {
        List<Purchase> GetAll();
        Purchase? GetByUid(Guid uid);
        Purchase Add(Purchase purchase);
        void Update(Purchase purchase);
        void Delete(Guid uid);
        List<Purchase> GetPurchasesByUser(Guid userUid);
    }
}
