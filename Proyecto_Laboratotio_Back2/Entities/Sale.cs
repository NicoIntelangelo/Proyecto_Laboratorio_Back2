using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;

namespace Proyecto_Laboratotio_Back2.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }

        [ForeignKey("userId")]
        public int UserId { get; set; } // Relación muchas ventas a 1 usuario
        public User User { get; set; }

        public ICollection<ProductsSales> ProductsSales { get; set; }
    }

}
