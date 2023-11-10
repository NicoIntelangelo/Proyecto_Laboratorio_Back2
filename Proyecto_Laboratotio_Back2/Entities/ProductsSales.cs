namespace Proyecto_Laboratotio_Back2.Entities
{
    public class ProductsSales
    {
        public int ProductId { get; set; }
        public Product Product{ get; set; }

        public int SaleId { get; set; }
        public Sale Sale { get; set; }
    }
}
