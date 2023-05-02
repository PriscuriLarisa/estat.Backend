using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IOrderProducts
    {
        List<OrderProduct> GetAll();
        OrderProduct? GetByUid(Guid uid);
        OrderProduct Add(OrderProduct orderProduct);
        void Update(OrderProduct orderProduct);
        void Delete(Guid uid);
        List<OrderProduct> GetOrderProductsByUser(Guid userUid);
        List<OrderProduct> GetOrderProductsByOrder(Guid orderUid);
    }
}