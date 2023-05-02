using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class ProductRequests : DALObject, IProductRequests
    {
        public ProductRequests(DatabaseContext context) : base(context)
        {
        }

        public ProductRequest Add(ProductRequest productRequest)
        {
            EntityEntry<ProductRequest>? addedProductRequest = _context.ProductRequests.Add(productRequest);
            _context.SaveChanges();
            return addedProductRequest.Entity;
        }

        public void Delete(Guid uid)
        {
            ProductRequest productRequest = _context.ProductRequests.FirstOrDefault(pr => pr.ProductRequestGUID == uid);
            if (productRequest == null)
                return;
            _context.ProductRequests.Remove(productRequest);
            _context.SaveChanges();
        }

        public List<ProductRequest> GetAll()
        {
            return _context.ProductRequests
                .Include(pr => pr.Product)
                .Include(pr => pr.User)
                .ToList();
        }

        public ProductRequest? GetByUid(Guid uid)
        {
            return _context.ProductRequests
                .Include(pr => pr.Product)
                .Include(pr => pr.User)
                .FirstOrDefault(pr => pr.ProductRequestGUID == uid);
        }

        public List<ProductRequest> GetProductRequestsByUser(Guid userUid)
        {
            return _context.ProductRequests
                .Include(pr => pr.Product)
                .Include(pr => pr.User)
                .Where(pr => pr.UserGUID == userUid)
                .ToList();
        }

        public void Update(ProductRequest productRequest)
        {
            ProductRequest oldProductRequest = _context.ProductRequests.FirstOrDefault(pr => pr.ProductRequestGUID == productRequest.ProductRequestGUID);
            if (oldProductRequest == null) return;

            oldProductRequest.ProductGUID = productRequest.ProductGUID;
            oldProductRequest.UserGUID = productRequest.UserGUID;
            oldProductRequest.Date = productRequest.Date;

            _context.ProductRequests.Update(oldProductRequest);
            _context.SaveChanges();
        }
    }
}
