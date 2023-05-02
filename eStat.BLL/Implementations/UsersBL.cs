using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class UsersBL : BusinessObject, IUsers
    {
        public UsersBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public User Add(UserCreate user)
        {
            return UserConverter.ToDTO(_dalContext.Users.Add(UserConverter.ToEntity(user)));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Users.Delete(uid);
        }

        public List<User> GetAll()
        {
            return _dalContext.Users.GetAll().Select(u => UserConverter.ToDTO(u)).ToList();
        }

        public List<UserInfo> GetAllInfo()
        {
            return _dalContext.Users.GetAll().Select(u => UserConverter.ToDTOInfo(u)).ToList();
        }

        public User? GetByUid(Guid uid)
        {
            return UserConverter.ToDTO(_dalContext.Users.GetByUid(uid));
        }

        public UserInfo GetUserInfo(Guid uid)
        {
            return UserConverter.ToDTOInfo(_dalContext.Users.GetByUid(uid));
        }

        public UserInfo GetByEmail(string email)
        {
            return UserConverter.ToDTOInfo(_dalContext.Users.GetByEmail(email));
        }

        public UserInfo Login(UserLogin userLogin)
        {
            DAL.Entities.User existingUser = _dalContext.Users.GetByEmail(userLogin.Email);
            if (existingUser == null) return null;
            return UserConverter.ToDTOInfo(existingUser);
        }

        public void Update(User user)
        {
            _dalContext.Users.Update(UserConverter.ToEntity(user));
        }
    }
}
