using com.sun.org.apache.xml.@internal.resolver.helpers;
using Proyecto_Laboratotio_Back2.Data;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;

namespace Proyecto_Laboratotio_Back2.Repository.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AplicationDbContext _context;

        public ProductRepository(AplicationDbContext context)
        {
            _context = context;
        }
        public List<Product> GetListProduct()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return _context.Products.SingleOrDefault(p => p.Id == id);
        }

        public void DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public Product AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return product;
        }
        
        public Product UpdateProductData(Product product)
        {
            var productItem = _context.Products.FirstOrDefault(p => p.Id == product.Id);

            if (productItem != null)
            {
                productItem.Brand = product.Brand;
                productItem.ProductName = product.ProductName;
                productItem.Price = product.Price;
                productItem.Discount = product.Discount;
                productItem.Category = product.Category;
                productItem.Sizes = product.Sizes;
                productItem.Image = product.Image;
                productItem.IsNewArticle = product.IsNewArticle;
               

                _context.SaveChanges();
                return product;
            }
            return null;
        }
    }
}
