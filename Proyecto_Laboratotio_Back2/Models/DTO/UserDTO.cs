using Proyecto_Laboratotio_Back2.Entities;

namespace Proyecto_Laboratotio_Back2.Models.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Direction { get; set; }
        public UserRole Role { get; set; }
    }
}
