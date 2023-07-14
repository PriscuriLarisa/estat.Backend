using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;
using Microsoft.AspNetCore.Identity;

namespace eStat.BLL.Implementations
{
    public class AuthenticationBL : BusinessObject, IAuthentication
    {
        public AuthenticationBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public UserInfo Login(UserLogin userLogin)
        {
            DAL.Entities.User retrievedUser = _dalContext.Users.GetAll().FirstOrDefault(u => u.Email == userLogin.Email);
            if(retrievedUser == null)
            {
                return null;
            }
            if(!BCrypt.Net.BCrypt.Verify(userLogin.Password, retrievedUser.Password))
            {
                return null;
            }
            UserInfo user = UserConverter.ToDTOInfo(retrievedUser);
            return user;
        }
    }
}