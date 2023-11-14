using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Laboratotio_Back2.Entities;
using Proyecto_Laboratotio_Back2.Models.DTO;
using Proyecto_Laboratotio_Back2.Repository.Implementations;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;
using System.Diagnostics.Contracts;
using System.Security.Claims;

namespace Proyecto_Laboratotio_Back2.Controllers
{
    [Route("sale")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ISaleRepository _saleRepository;
        private readonly IProductSaleRepository _productSaleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public SaleController(IConfiguration config, IMapper mapper, ISaleRepository saleRepository, IProductSaleRepository productSaleRepository, IProductRepository productRepository)
        {
            _config = config;
            this._saleRepository = saleRepository;
            this._productSaleRepository = productSaleRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult PostProduct(SaleDTO saleDTO)
        {
            try
            {
                int userId = Int32.Parse(HttpContext.User.Claims.SingleOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);//valor del id del user logueado


                Sale sale = new Sale
                {
                    SaleDate= DateTime.Now,
                    UserId = userId,
                };


                var saleAdded = _saleRepository.AddSale(sale);

                List<ProductDTO> products = new List<ProductDTO>();

                foreach (var productId in saleDTO.ProdcutsIds)
                {

                    ProductsSales productSale = new ProductsSales
                    {
                        ProductId = productId,
                        SaleId = saleAdded.Id,
                    };

                    _productSaleRepository.AddProductSale(productSale);
                    var product = _productRepository.GetProduct(productId);
                    var productItemDto = _mapper.Map<ProductDTO>(product);
                    products.Add(productItemDto);
                }

                

                return Created("Created", products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
