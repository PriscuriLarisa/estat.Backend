using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class OrdersBL : BusinessObject, IOrders
    {
        public OrdersBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Order Add(Order order)
        {
            return OrderConverter.ToDTO(_dalContext.Orders.Add(OrderConverter.ToEntity(order)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Orders.Delete(uid);
        }

        public List<Order> GetAll()
        {
            return _dalContext.Orders.GetAll().Select(o => OrderConverter.ToDTO(o)).ToList();
        }

        public Order? GetByUid(Guid uid)
        {
            return OrderConverter.ToDTO(_dalContext.Orders.GetByUid(uid));
        }

        public List<Order> GetPurchasesByUser(Guid userUid)
        {
            return _dalContext.Orders.GetOrdersByUser(userUid).Select(o => OrderConverter.ToDTO(o)).ToList();
        }

        public void Update(Order order)
        {
            _dalContext.Orders.Update(OrderConverter.ToEntity(order));
        }
    }
}