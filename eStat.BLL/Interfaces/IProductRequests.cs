using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IProductRequests
    {
        List<ProductRequest> GetAll();
        ProductRequest? GetByUid(Guid uid);
        ProductRequest Add(ProductRequest assignment);
        void Update(ProductRequest assignment);
        void Delete(Guid uid);
        List<ProductRequest> GetUsersByUser(Guid userUid);
    }
}