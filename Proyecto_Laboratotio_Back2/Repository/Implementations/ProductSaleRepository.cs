using Proyecto_Laboratotio_Back2.Data;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;

namespace Proyecto_Laboratotio_Back2.Repository.Implementations
{
    public class ProductSaleRepository : IProductSaleRepository
    {
        private readonly AplicationDbContext _context;

        public ProductSaleRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public List<ProductsSales> GetListProductsSales()
        {
            return _context.ProductsSales.ToList();
        }
        //public List<ProductsSales> GetProductsOfSalesOfUser(int user_id)
        //{
        //    return _context.ProductsSales.ToList().Where(ps => ps.);
        //}
        public ProductsSales AddProductSale(ProductsSales productsSales)
        {
            _context.ProductsSales.Add(productsSales);
            _context.SaveChanges();
            return productsSales;
        }
    }
}
