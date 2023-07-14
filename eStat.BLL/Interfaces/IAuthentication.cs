using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IAuthentication
    {
        UserInfo Login(UserLogin userLogin);
    }
}