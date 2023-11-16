using AutoMapper;
using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;
using Proyecto_Laboratotio_Back2.Repository.Implementations;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;
using System.Security.Claims;

namespace Proyecto_Laboratotio_Back2.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ProductController(IConfiguration config, IProductRepository productRepository, IUserRepository userRepository, IMapper mapper)
        {
            _config = config;
            this._productRepository = productRepository;
            this._userRepository = userRepository;
            _mapper = mapper;

        }

        [HttpGet("all")]
        public IActionResult GetProducts()
        {
            try
            {
                var Products = _productRepository.GetListProduct();

                var ProductsDTO = _mapper.Map<IEnumerable<ProductDTO>>(Products);

                return Ok(ProductsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("new")]
        public IActionResult GetProductsNew()
        {
            try
            {
                var Products = _productRepository.GetListProduct().Where(p => p.IsNewArticle == true);

                var ProductsDTO = _mapper.Map<IEnumerable<ProductDTO>>(Products);

                return Ok(ProductsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{category}")]
        public IActionResult GetProductsForCategory(string category)
        {
            try
            {
                var Products = _productRepository.GetListProduct().Where(p => p.Category == category);

                var ProductsDTO = _mapper.Map<IEnumerable<ProductDTO>>(Products);

                return Ok(ProductsDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("id/{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var Product = _productRepository.GetProduct(id);

                var ProductDTO = _mapper.Map<ProductDTO>(Product);

                return Ok(ProductDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult PostProduct(ProductDTOCreation productDtoCreation)
        {
            int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            
            var userRole = _userRepository.GetUser(userId).Role;

            if (userRole == UserRole.Admin || userRole == UserRole.SuperAdmin)
            {
                try
                {
                    var product = _mapper.Map<Product>(productDtoCreation);

                    var productItem = _productRepository.AddProduct(product);

                    var productItemDto = _mapper.Map<ProductDTO>(productItem);

                    return Created("Created", productItemDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Unauthorized("Permisos Insuficientes");
           
        }

        [HttpPut]
        [Authorize]
        public IActionResult PutProduct(ProductDTO productDto)
        {
            
            int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userRole = _userRepository.GetUser(userId).Role;

            if (userRole == UserRole.Admin || userRole == UserRole.SuperAdmin)
            {
                try
                {
                    var product = _mapper.Map<Product>(productDto);

                    var productItem = _productRepository.UpdateProductData(product);

                    var productItemDto = _mapper.Map<ProductDTO>(productItem);

                    return Ok(productItemDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Unauthorized("Permisos Insuficientes");

        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult DeleteProduct(int id)
        {

            int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var userRole = _userRepository.GetUser(userId).Role;

            if (userRole == UserRole.Admin || userRole == UserRole.SuperAdmin)
            {
                try
                {

                    var productForDelete = _productRepository.GetProduct(id);


                    if (productForDelete == null)
                    {
                        return NotFound();
                    }

                    _productRepository.DeleteProduct(productForDelete);

                    return Ok();

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
