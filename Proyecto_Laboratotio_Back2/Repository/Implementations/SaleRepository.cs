using com.sun.org.apache.xml.@internal.resolver.helpers;
using com.sun.xml.@internal.bind.v2.model.core;
using Proyecto_Laboratotio_Back2.Data;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;

namespace Proyecto_Laboratotio_Back2.Repository.Implementations
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AplicationDbContext _context;

        public SaleRepository(AplicationDbContext context)
        {
            _context = context;
        }

        public List<Sale> GetListSale()
        {
            return _context.Sales.ToList();
        }
        
        public Sale GetSaleOfUser(int user_id)
        {
            return _context.Sales.SingleOrDefault(s => s.UserId == user_id);
        }

        public List<Sale> GetSalesOfUser(int user_id)
        {
            return _context.Sales.Where(s => s.UserId == user_id).ToList();
        }
        public void DeleteSale(Sale sale)
        {
            _context.Sales.Remove(sale);
            _context.SaveChanges();
        }
        public Sale AddSale(Sale sale)
        {
            _context.Sales.Add(sale);
            _context.SaveChanges();
            return sale;
        }
    }
}
