using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IPurchases
    {
        List<Purchase> GetAll();
        Purchase? GetByUid(Guid uid);
        Purchase Add(Purchase purchase);
        Purchase AddPurchase(Guid shoppingCartUid);
        Purchase AddPurchaseWithAddress(Guid shoppingCartUid, string address);
        Purchase AddPurchaseWithDate(Guid shoppingCartUid, DateTime date);
        Purchase AddPurchaseWithDateAndAddress(Guid shoppingCartUid, DateTime date, string address);
        void Update(Purchase purchase);
        void Delete(Guid uid);
        List<Purchase> GetByUser(Guid userUid);
    }
}