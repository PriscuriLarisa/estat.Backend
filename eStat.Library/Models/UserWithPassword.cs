using eStat.Common.Enums;

namespace eStat.Library.Models
{
    public class UserWithPassword
    {
        public Guid UserGUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime Birthday { get; set; }
        public Memberships Membership { get; set; }
    }
}