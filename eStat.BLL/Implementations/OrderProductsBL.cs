using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    internal class OrderProductsBL : BusinessObject, IOrderProducts

    {
        public OrderProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public OrderProduct Add(OrderProduct orderProduct)
        {
            return OrderProductConverter.ToDTO(_dalContext.OrderProducts.Add(OrderProductConverter.ToEntity(orderProduct)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.OrderProducts.Delete(uid);
        }

        public List<OrderProduct> GetAll()
        {
            return _dalContext.OrderProducts.GetAll().Select(p => OrderProductConverter.ToDTO(p)).ToList();
        }

        public OrderProduct? GetByUid(Guid uid)
        {
            return OrderProductConverter.ToDTO(_dalContext.OrderProducts.GetByUid(uid));
        }

        public List<OrderProduct> GetOrderProductsByOrder(Guid orderUid)
        {
            return _dalContext.OrderProducts.GetByOrder(orderUid).Select(p => OrderProductConverter.ToDTO(p)).ToList();
        }

        public List<OrderProduct> GetOrderProductsByUser(Guid userUid)
        {
            return _dalContext.OrderProducts.GetByUser(userUid).Select(p => OrderProductConverter.ToDTO(p)).ToList();
        }

        public void Update(OrderProduct orderProduct)
        {
            _dalContext.OrderProducts.Update(OrderProductConverter.ToEntity(orderProduct));
        }
    }
}
