using System.ComponentModel.DataAnnotations;

namespace Proyecto_Laboratotio_Back2.Models.DTO
{
    public class AuthenticationRequestBody
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
