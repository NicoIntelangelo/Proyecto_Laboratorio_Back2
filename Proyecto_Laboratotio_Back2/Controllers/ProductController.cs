using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;
using Proyecto_Laboratotio_Back2.Repository.Implementations;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;

namespace Proyecto_Laboratotio_Back2.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IConfiguration config, IProductRepository productRepository, IMapper mapper)
        {
            _config = config;
            this._productRepository = productRepository;
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

        [HttpPut]
        public IActionResult PutProduct(ProductDTO productDto)
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
    }
}
