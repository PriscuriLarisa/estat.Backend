
namespace eStat.Library.Converters
{
    public static class UserConverter
    {
        public static Library.Models.User ToDTO(DAL.Entities.User user)
        {
            return new Library.Models.User
            {
                UserGUID = user.UserGUID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                Role = user.Role,
                Membership = user.Membership,
                Products = user.Products.Select(p => UserProductConverter.ToDTO(p)).ToList(),
                Orders = user.Orders.Select(p => OrderConverter.ToDTO(p)).ToList()
            };
        }

        public static DAL.Entities.User ToEntity(Library.Models.User user)
        {
            return new DAL.Entities.User
            {
                UserGUID = user.UserGUID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                Role = user.Role,
                Membership = user.Membership
            };
        }

        public static DAL.Entities.User ToEntity(Library.Models.UserCreate user)
        {
            return new DAL.Entities.User
            {
                UserGUID = Guid.Empty,
                FirstName = "",
                LastName = "",
                Birthday = DateTime.Now,
                Email = user.Email,
                Role = user.Role,
                Membership = Common.Enums.Memberships.NoMembership,
                Password = user.Password,
                Orders = new List<DAL.Entities.Order>(),
                Products = new List<DAL.Entities.UserProduct>()
            };
        }

        public static DAL.Entities.User ToEntity(Library.Models.UserInfo user)
        {
            return new DAL.Entities.User
            {
                UserGUID = user.UserGUID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                Role = user.Role,
                Membership = user.Membership
            };
        }

        public static Library.Models.UserWithPassword ToDTOWithPassword(DAL.Entities.User user)
        {
            return new Library.Models.UserWithPassword
            {
                UserGUID = user.UserGUID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                Role = user.Role,
                Membership = user.Membership,
                Password = user.Password
            };
        }

        public static Library.Models.UserInfo ToDTOInfo(DAL.Entities.User user)
        {
            return new Library.Models.UserInfo
            {
                UserGUID = user.UserGUID,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Email = user.Email,
                Role = user.Role,
                Membership = user.Membership
            };
        }
    }
}
