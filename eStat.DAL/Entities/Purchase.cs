using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eStat.DAL.Entities
{
    public class Purchase
    {
        [Key]
        public Guid PurchaseGUID { get; set; }
        [ForeignKey("User")]
        public Guid? UserGUID { get; set; }
        public User? User { get; set; }
        public DateTime Date { get; set; }
        public List<PurchaseProduct> Products { get; set; }
        public string Address { get; set; }
    }
}