using AutoMapper;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;

namespace Proyecto_Laboratotio_Back2.Models.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<User, UserDTOCreation>();
            CreateMap<UserDTOCreation, User>();

            CreateMap<User, AdminDTOCreation>();
            CreateMap<AdminDTOCreation, User>();

            CreateMap<User, UserDTOEdit>();
            CreateMap<UserDTOEdit, User>();

        }
    }
}
