using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IProductRequests
    {
        List<ProductRequest> GetAll();
        ProductRequest? GetByUid(Guid uid);
        ProductRequest Add(ProductRequest productRequest);
        void Update(ProductRequest productRequest);
        void Delete(Guid uid);
        List<ProductRequest> GetProductRequestsByUser(Guid userUid);
    }
}