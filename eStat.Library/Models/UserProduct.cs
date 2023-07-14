using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.Library.Models
{
    public class UserProduct
    {
        public Guid UserProductGUID { get; set; }
        public Guid UserGUID { get; set; }
        public UserInfo? User { get; set; }
        public Guid ProductGUID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
    }
}
