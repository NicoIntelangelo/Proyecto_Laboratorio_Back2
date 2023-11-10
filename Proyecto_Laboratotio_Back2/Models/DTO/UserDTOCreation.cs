using Proyecto_Laboratotio_Back2.Entities;

namespace Proyecto_Laboratotio_Back2.Models.DTO
{
    public class UserDTOCreation
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }

    }
}
