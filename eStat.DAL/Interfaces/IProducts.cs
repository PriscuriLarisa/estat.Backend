using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IProducts
    {
        List<Product> GetAll();
        List<string> GetProductsCategories();
        Product? GetByUid(Guid uid);
        Product Add(Product product);
        void Update(Product product);
        void Delete(Guid uid);
        void PutInUse(Guid productGuid);
        void PutOutOfUse(Guid productGuid);
    }
}