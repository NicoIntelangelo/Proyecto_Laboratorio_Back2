using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;

namespace Proyecto_Laboratotio_Back2.Repository.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetListUser();
        User GetUser(int id);
        void DeleteUser(User user);
        User AddUser(User user);
        public void UpdateUserData(User user);
        public User? ValidateUser(AuthenticationRequestBody authRequestBody);
    }
}
