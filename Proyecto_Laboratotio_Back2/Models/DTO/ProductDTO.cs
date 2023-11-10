namespace Proyecto_Laboratotio_Back2.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Sizes { get; set; }
        public double Price { get; set; }
        public int Discount { get; set; }
        public string Image { get; set; }
        public bool IsNewArticle { get; set; }
    }
}
