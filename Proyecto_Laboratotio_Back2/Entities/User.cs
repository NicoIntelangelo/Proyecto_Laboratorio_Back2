using System.Diagnostics.Contracts;

namespace Proyecto_Laboratotio_Back2.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Direction { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; }
        public IEnumerable<Sale> Sales { get; set; }
    }
}