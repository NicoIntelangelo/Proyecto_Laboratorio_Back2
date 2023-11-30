using AutoMapper;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;

namespace Proyecto_Laboratotio_Back2.Models.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<ProductsSales, SaleDTO>();
            CreateMap<SaleDTO, ProductsSales>();

            CreateMap<ProductsSales, SaleDTOProducts>();
            CreateMap<SaleDTOProducts, ProductsSales>();

        }
    }
}
