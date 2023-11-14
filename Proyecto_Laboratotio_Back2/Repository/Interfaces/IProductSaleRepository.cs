using Proyecto_Laboratotio_Back2.Entities;

namespace Proyecto_Laboratotio_Back2.Repository.Interfaces
{
    public interface IProductSaleRepository
    {
        List<ProductsSales> GetListProductsSales();
        //List<ProductsSales> GetProductsOfSalesOfUser(int user_id);
        ProductsSales AddProductSale(ProductsSales productsSales);
    }
}
