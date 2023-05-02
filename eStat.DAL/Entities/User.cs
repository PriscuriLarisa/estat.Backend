
using eStat.Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace eStat.DAL.Entities
{
    public class User
    {
        [Key]
        public Guid UserGUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime Birthday { get; set; }
        public Memberships Membership { get; set; }
        public List<UserProduct> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}