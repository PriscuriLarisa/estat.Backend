using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IOrders
    {
        List<Order> GetAll();
        Order? GetByUid(Guid uid);
        Order Add(Order assignment);
        void Update(Order assignment);
        void Delete(Guid uid);
        List<Order> GetOrdersByUser(Guid userUid);
    }
}
