using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;
using System.Security.Claims;

namespace Proyecto_Laboratotio_Back2.Controllers
{
    [Route("super")]
    [ApiController]
    [Authorize]
    public class SuperAdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public SuperAdminController(IConfiguration config, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _config = config;
            this._productRepository = productRepository;
            this._userRepository = userRepository;
            _mapper = mapper;
        }
        
        [HttpPost]
        public IActionResult PostAdmin(AdminDTOCreation adminDtoCreation)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userRole = _userRepository.GetUser(userId).Role;

            if (userRole == UserRole.SuperAdmin)
            {
                try
                {
                    var admin = _mapper.Map<User>(adminDtoCreation);

                    admin.Role = UserRole.Admin; //EL SUPER ADMIN CREA ADMINS

                    var usersActivos = _userRepository.GetListUser();

                    foreach (var userActivo in usersActivos)
                    {
                        if (admin.Email == userActivo.Email)
                        {
                            return BadRequest("El email ingresado ya es utilizado en una cuenta activa");
                        }
                    }

                    var userItem = _userRepository.AddUser(admin);

                    var userItemDto = _mapper.Map<UserDTO>(userItem);

                    return Created("Created", userItemDto); ///*************
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Unauthorized("Permisos Insuficientes");
            
        }

        [HttpGet]
        public IActionResult GetAdmins()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userRole = _userRepository.GetUser(userId).Role;

            if (userRole == UserRole.SuperAdmin)
            {
                try
                {
                    var Admins = _userRepository.GetListUser().Where(u => u.Role == UserRole.Admin);

                    var AdminsDTO = _mapper.Map<IEnumerable<UserDTO>>(Admins);

                    return Ok(AdminsDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Unauthorized("Permisos Insuficientes");
        }

        [HttpGet("admins-list")]
        public IActionResult GetAdminsList()
        {
            int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userRole = _userRepository.GetUser(userId).Role;

            if (userRole == UserRole.SuperAdmin)
            {
                try
                {
                    var admins = _userRepository.GetListAdmins();

                    var adminsDTO = _mapper.Map<IEnumerable<UserDTO>>(admins);

                    return Ok(adminsDTO);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }

            return Unauthorized("Permisos Insuficientes");
        }


    }
}