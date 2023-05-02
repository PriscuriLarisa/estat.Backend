using eStat.DAL.Core;
using eStat.DAL.Entities;
using eStat.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace eStat.DAL.Implementations
{
    public class Users : DALObject, IUsers
    {
        public Users(DatabaseContext context) : base(context)
        {
        }

        public User Add(User user)
        {
            EntityEntry<User>? addedUser = _context.Users.Add(user);
            _context.SaveChanges();
            return addedUser.Entity;
        }

        public void Delete(Guid uid)
        {
            DeleteUser(uid);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users
                .Include(user => user.Products)
                    .ThenInclude(userProduct => userProduct.Product)
                .Include(user => user.Orders)
                    .ThenInclude(order => order.Products)
                .ToList();
        }

        public User? GetByUid(Guid uid)
        {
            return _context.Users
                .Include(user => user.Products)
                    .ThenInclude(userProduct => userProduct.Product)
                .Include(user => user.Orders)
                    .ThenInclude(order => order.Products)
                .FirstOrDefault(user => user.UserGUID == uid);
        }

        public User GetByEmail(string email)
        {
            return _context.Users
                .FirstOrDefault(user => user.Email.Equals(email));
        }

        public void Update(User user)
        {
            User oldUser = _context.Users.FirstOrDefault(u => u.UserGUID == user.UserGUID);
            if (oldUser == null) return;
            oldUser.FirstName = user.FirstName;
            oldUser.LastName = user.LastName;
            oldUser.Role = user.Role;
            oldUser.Email = user.Email;
            oldUser.Membership = user.Membership;
            oldUser.Birthday = user.Birthday;
            oldUser.Products = user.Products;
            oldUser.Orders = user.Orders;

            _context.Users.Update(oldUser);
            _context.SaveChanges();
        }

        public void UpdateUserPassword(Guid userGuid, string password)
        {
            Entities.User oldUser = _context.Users.FirstOrDefault(u => u.UserGUID == userGuid);
            if (oldUser == null) return;
            oldUser.Password = password; 

            _context.Users.Update(oldUser);
            _context.SaveChanges();
        }

        private void DeleteUser(Guid uid)
        {
            User user = _context.Users.Include(user => user.Products).FirstOrDefault(user => user.UserGUID == uid);
            if (user == null)
                return;

            foreach (UserProduct userProduct in user.Products)
            {
                _context.Entry(userProduct).State = EntityState.Deleted;
            }
            _context.Users.Remove(user);
        }
    }
}