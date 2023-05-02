using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IUsers
    {
        List<User> GetAll();
        List<UserInfo> GetAllInfo();
        User Add(UserCreate user);
        void Delete(Guid uid);
        void Update(User user);
        User? GetByUid(Guid uid);
        UserInfo? GetUserInfo(Guid uid);
        UserInfo GetByEmail(string email);
        UserInfo Login(UserLogin userLogin);
    }
}