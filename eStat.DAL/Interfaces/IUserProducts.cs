using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IUserProducts
    {
        List<UserProduct> GetAll();
        UserProduct? GetByUid(Guid uid);
        UserProduct Add(UserProduct userProduct);
        void Update(UserProduct userProduct);
        void Delete(Guid uid);
        List<UserProduct> GetUserProductsByUser(Guid userUid);
        List<UserProduct> GetUserProductsByProduct(Guid userUid);
    }
}
