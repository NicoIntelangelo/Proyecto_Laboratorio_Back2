using Proyecto_Laboratotio_Back2.Entities;

namespace Proyecto_Laboratotio_Back2.Repository.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetListProduct();
        Product GetProduct(int id);
        void DeleteProduct(Product product);
        Product AddProduct(Product product);
        public void UpdateProductData(Product product);
    }
}
