using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class UserProductsBL : BusinessObject, IUserProducts
    {
        public UserProductsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public UserProduct Add(UserProduct userProduct)
        {
            return UserProductConverter.ToDTO(_dalContext.UserProducts.Add(UserProductConverter.ToEntity(userProduct)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.UserProducts.Delete(uid);
        }

        public List<UserProduct> GetAll()
        {
            return _dalContext.UserProducts.GetAll().Select(p => UserProductConverter.ToDTO(p)).ToList();
        }

        public UserProduct? GetByUid(Guid uid)
        {
            return UserProductConverter.ToDTO(_dalContext.UserProducts.GetByUid(uid));
        }

        public List<UserProduct> GetUserProductsByProduct(Guid productUid)
        {
            var x = _dalContext.UserProducts.GetUserProductsByProduct(productUid);
            return _dalContext.UserProducts.GetUserProductsByProduct(productUid).Select(p => UserProductConverter.ToDTOWithUser(p)).ToList();
        }

        public List<UserProduct> GetUserProductsByProductInBatches(Guid userUid, int batchNb)
        {
            List<UserProduct> allProducts = GetUserProductsByUser(userUid);
            return allProducts.Skip(batchNb * 20).Take(20).ToList();
        }

        public List<UserProduct> GetUserProductsByUser(Guid userUid)
        {
            return _dalContext.UserProducts.GetUserProductsByUser(userUid).Select(p => UserProductConverter.ToDTO(p)).ToList();
        }

        public void Update(UserProduct userProduct)
        {
            _dalContext.UserProducts.Update(UserProductConverter.ToEntity(userProduct));
        }
    }
}
