using eStat.Common.Enums;

namespace eStat.Library.Models
{
    public class UserCreate
    {
        public string Email { get; set; }
        public Roles Role { get; set; }
        public string Password { get; set; }
    }
}
