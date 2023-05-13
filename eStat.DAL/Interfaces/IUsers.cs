using eStat.DAL.Entities;

namespace eStat.DAL.Interfaces
{
    public interface IUsers
    {
        List<User> GetAll();
        User? GetByUid(Guid uid);
        User Add(User user);
        void Update(User user);
        void Delete(Guid uid);
        void UpdateUserInfo(User user);
        User GetByEmail(string email);
        void UpdateUserPassword(Guid userGuid, string password);
    }
}