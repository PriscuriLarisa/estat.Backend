using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class OrderProducts : DALObject, IOrderProducts
    {
        public OrderProducts(DatabaseContext context) : base(context)
        {
        }

        public OrderProduct Add(OrderProduct orderProduct)
        {
            EntityEntry<OrderProduct>? addedOrderProduct = _context.OrderProducts.Add(orderProduct);
            _context.SaveChanges();
            return addedOrderProduct.Entity;
        }

        public void Delete(Guid uid)
        {
            OrderProduct orderProduct = _context.OrderProducts.FirstOrDefault(op => op.OrderProductGUID == uid);
            if (orderProduct == null)
                return;

            _context.OrderProducts.Remove(orderProduct);
            _context.SaveChanges();
        }

        public List<OrderProduct> GetAll()
        {
            return _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                .ToList();
        }

        public OrderProduct? GetByUid(Guid uid)
        {
            return _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                .FirstOrDefault(op => op.OrderProductGUID == uid);
        }

        public List<OrderProduct> GetByOrder(Guid orderUid)
        {
            return _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                .Where(op => op.OrderGUID == orderUid)
                .ToList();
        }

        public void Update(OrderProduct orderProduct)
        {
            OrderProduct oldOrderProduct = _context.OrderProducts.FirstOrDefault(op => op.OrderProductGUID == orderProduct.OrderProductGUID);
            if (oldOrderProduct == null) return;
            oldOrderProduct.ProductGUID = orderProduct.ProductGUID;
            oldOrderProduct.OrderGUID = orderProduct.OrderGUID;
            oldOrderProduct.Price = orderProduct.Price;

            _context.OrderProducts.Update(oldOrderProduct);
            _context.SaveChanges();
        }

        public List<OrderProduct> GetByUser(Guid userUid)
        {
            return _context.OrderProducts
                .Include(op => op.Product)
                .Include(op => op.Order)
                .Where(op => op.Order.UserGUID == userUid)
                .ToList();
        }
    }
}
