﻿using AutoMapper;
using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;
using System.Security.Claims;

namespace Proyecto_Laboratotio_Back2.Controllers
{
    [Route("user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet("role")]
        public IActionResult GetUserRole()
        {
            try
            {
                int Id = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var userItem = _userRepository.GetUser(Id).Role;

                return Ok(userItem); ///*************
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var userSesionRole = _userRepository.GetUser(userSesionId).Role;

                var userForDelete = _userRepository.GetUser(id);


                if (userForDelete == null)
                {
                    return NotFound();
                }
                
                if (id != userSesionId && userSesionRole != UserRole.SuperAdmin) //cheque que el usuario se elimine a el mismo o que un superadmin lo haga
                {
                    return Unauthorized();
                }

               

                _userRepository.DeleteUser(userForDelete);

                //return NoContent();
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPut("{id}")] 
        public IActionResult EditUserData(int id, UserDTOEdit userDTOEdit)
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var userSesionRole = _userRepository.GetUser(userSesionId).Role;

                var user = _mapper.Map<User>(userDTOEdit);

                if (id != userSesionId && userSesionRole == UserRole.User) //cheque que el usuario se edite a el mismo o que un admin o superadmin lo haga
                {
                    return Unauthorized();
                }

                if (id != user.Id)
                {
                    return Unauthorized();
                }

                var userItem = _userRepository.GetUser(id);

                if (userItem == null)
                {
                    return NotFound();
                }

                _userRepository.UpdateUserData(user);

                var userModificado = _userRepository.GetUser(id);

                var userModificadoDto = _mapper.Map<UserDTO>(userModificado);

                return Ok(userModificadoDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("newadmin")]
        public IActionResult PostUser(UserDTOCreation userDtoCreation)
        {
            try
            {
                int userSesionId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                var userSesionRole = _userRepository.GetUser(userSesionId).Role;

                if (userSesionRole != UserRole.SuperAdmin) 
                {
                    return Unauthorized();
                }

                var user = _mapper.Map<User>(userDtoCreation);

                user.Role = UserRole.Admin; //crea el user como admin

                var usersActivos = _userRepository.GetListUser();

                foreach (var userActivo in usersActivos)
                {
                    if (user.Email == userActivo.Email)
                    {
                        return BadRequest("El email ingresado ya es utilizado en una cuenta activa");
                    }
                }

                var userItem = _userRepository.AddUser(user);

                var userItemDto = _mapper.Map<UserDTO>(userItem);

                return Created("Created", userItemDto); ///*************
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
