using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderGUID { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("User")]
        public Guid? UserGUID { get; set; }
        public User? User { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}
