using AutoMapper;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;

namespace Proyecto_Laboratotio_Back2.Models.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();

            CreateMap<Product, ProductDTOCreation>();
            CreateMap<ProductDTOCreation, Product>();


        }
    }
}
