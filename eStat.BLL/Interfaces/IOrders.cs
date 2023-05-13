using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IOrder
    {
        List<Order> GetAll();
        Order? GetByUid(Guid uid);
        Order Add(Order order);
        void Update(Order order);
        void Delete(Guid uid);
        List<Order> GetPurchasesByUser(Guid userUid);
    }
}