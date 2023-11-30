using Proyecto_Laboratotio_Back2.Entities;

namespace Proyecto_Laboratotio_Back2.Models.DTO
{
    public class SaleDTOProducts
    {
        public int SaleId { get; set; }
        public List<ProductDTO> Products { get; set; }
        public float Price { get; set; }
        public DateTime SaleDate { get; set; }

    }
}
