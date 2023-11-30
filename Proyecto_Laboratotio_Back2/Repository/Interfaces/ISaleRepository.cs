using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;

namespace Proyecto_Laboratotio_Back2.Repository.Interfaces
{
    public interface ISaleRepository
    {
        List<Sale> GetListSale();
        Sale GetSaleOfUser(int user_id);
        void DeleteSale(Sale sale);
        Sale AddSale(Sale sale);
        List<Sale> GetSalesOfUser(int user_id);
    }
}
