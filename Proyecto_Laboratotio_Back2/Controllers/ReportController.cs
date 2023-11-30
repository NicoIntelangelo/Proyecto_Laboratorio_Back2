using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Laboratotio_Back2.Repository.Interfaces;
using System.Linq.Expressions;

namespace Proyecto_Laboratotio_Back2.Controllers
{
    [Route("informe")]
    [ApiController]
    public class ReportController : Controller
    {
        private readonly IProductRepository _productRepository;

        public ReportController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        [HttpGet("products")]
        public IActionResult GetInformeProductsPDF()
        {
                var products = _productRepository.GetListProduct();

                // Genera el PDF utilizando iText
                using (var pdfStream = new MemoryStream())
                {
                    using (var writer = new PdfWriter(pdfStream))
                    {
                        using (var pdf = new PdfDocument(writer))
                        {
                            var document = new Document(pdf);

                            document.Add(new Paragraph("Informe de Productos"));

                            var table = new Table(8); // Ajusta el número de columnas según tu modelo
                            table.AddHeaderCell("ID");
                            table.AddHeaderCell("Brand");
                            table.AddHeaderCell("ProductName");
                            table.AddHeaderCell("Category");
                            table.AddHeaderCell("Sizes");
                            table.AddHeaderCell("Price");
                            table.AddHeaderCell("Discount");
                            table.AddHeaderCell("New?");

                            foreach (var product in products)
                            {
                                table.AddCell(product.Id.ToString());
                                table.AddCell(product.Brand);
                                table.AddCell(product.ProductName);
                                table.AddCell(product.Category);
                                table.AddCell(product.Sizes);
                                table.AddCell(product.Price.ToString());
                                table.AddCell(product.Discount.ToString());
                                table.AddCell(product.IsNewArticle.ToString());
                            }

                            document.Add(table);
                        }
                    }

                    // Devuelve el PDF como un archivo de descarga
                    return File(pdfStream.ToArray(), "application/pdf", "InformeProductos.pdf");
                }
        }
    }
}