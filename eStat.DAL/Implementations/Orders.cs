using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class Orders : DALObject, IOrders
    {
        public Orders(DatabaseContext context) : base(context)
        {
        }

        public Order Add(Order order)
        {
            EntityEntry<Order>? addedOrder = _context.Orders.Add(order);
            _context.SaveChanges();
            return addedOrder.Entity;
        }

        public void Delete(Guid uid)
        {
            DeleteOrder(uid);
            _context.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return _context.Orders
                .Include(order => order.Products)
                    .ThenInclude(OrderProduct => OrderProduct.Product)
                .Include(order => order.User)
                .ToList();
        }

        public Order? GetByUid(Guid uid)
        {
            return _context.Orders
                .Include(order => order.Products)
                .ThenInclude(OrderProduct => OrderProduct.Product)
                .Include(order => order.User)
                .FirstOrDefault(user => user.UserGUID == uid);
        }

        public List<Order> GetOrdersByUser(Guid userUid)
        {
            return _context.Orders
                .Include(order => order.Products)
                .ThenInclude(OrderProduct => OrderProduct.Product)
                .Include(order => order.User)
                .Where(user => user.UserGUID == userUid)
                .ToList();
        }

        public void Update(Order order)
        {
            Order oldOrder = _context.Orders.FirstOrDefault(o => o.OrderGUID == order.OrderGUID);
            if (oldOrder == null) return;
            oldOrder.UserGUID = order.UserGUID;
            oldOrder.Products = order.Products;

            _context.Orders.Update(oldOrder);
            _context.SaveChanges();
        }

        private void DeleteOrder(Guid uid)
        {
            Order order = _context.Orders.Include(order => order.Products).FirstOrDefault(order => order.UserGUID == uid);
            if (order == null)
                return;

            foreach (OrderProduct orderProduct in order.Products)
            {
                _context.Entry(orderProduct).State = EntityState.Deleted;
            }
            _context.Orders.Remove(order);
        }
    }
}