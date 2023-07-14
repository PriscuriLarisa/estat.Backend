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
            var existingUser = GetAll().FirstOrDefault(u => u.Email == user.Email);
            if(existingUser != null)
            {
                return null;
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            var createdUser = UserConverter.ToDTO(_dalContext.Users.Add(UserConverter.ToEntity(user)));

            ShoppingCart shoppingCart = new ShoppingCart
            {
                ShoppingCartGUID = Guid.Empty,
                UserGUID = createdUser.UserGUID,
                Products = new List<ShoppingCartProduct>(),
                User = null
            };
            _dalContext.ShoppingCarts.Add(ShoppingCartConverter.ToEntity(shoppingCart));

            Notification notif= new Notification
            {
                NotificationGUID = Guid.Empty,
                Title = "Configure profile",
                Text = "You are advised to set up your profile before continuing.",
                Read = false,
                Hyperlink = "/manageProfile",
                HyperlinkText = "Go to my profile",
                UserGUID = createdUser.UserGUID,
                Date = DateTime.Now,
            };
            _dalContext.Notifications.Add(NotificationConverter.ToEntity(notif));

            return createdUser;
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

        public void UpdateUserInfo(UserInfo user)
        {
            _dalContext.Users.UpdateUserInfo(UserConverter.ToEntity(user));
        }

        public UserInfo GetUserByEmailAndPassword(UserLogin userLogin)
        {
            return UserConverter.ToDTOInfo(_dalContext.Users.GetAll().FirstOrDefault(u => u.Email == userLogin.Email && u.Password == userLogin.Password));
        }
    }
}
