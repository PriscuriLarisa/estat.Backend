using eStat.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.Library.Models
{
    public class UserInfo
    {
        public Guid UserGUID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Roles Role { get; set; }
        public DateTime Birthday { get; set; }
        public Memberships Membership { get; set; }
    }
}
