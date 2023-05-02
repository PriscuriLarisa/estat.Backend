using eStat.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.DAL.Interfaces
{
    public interface IOrderProducts
    {
        List<OrderProduct> GetAll();
        OrderProduct? GetByUid(Guid uid);
        OrderProduct Add(OrderProduct orderProduct);
        void Update(OrderProduct orderProduct);
        void Delete(Guid uid);
        List<OrderProduct> GetByOrder(Guid orderUid);
        List<OrderProduct> GetByUser(Guid userUid);
    }
}
