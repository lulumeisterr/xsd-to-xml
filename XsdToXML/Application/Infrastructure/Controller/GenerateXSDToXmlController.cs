using br.com.dev.xsd.Application.Services.XsdServices.Interface;
using br.com.dev.xsd.Infrastructure.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace br.com.dev.xsd.Infrastructure.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenerateXSDToXmlController : ControllerBase
    {
        private IReadXSDService _xsdService;
        public GenerateXSDToXmlController(IReadXSDService xsdService)
        {
            _xsdService = xsdService;
        }

        [HttpPost("xsd")]
        public async Task<IActionResult> GenerateXSDToXml([FromBody] XsdRequestDTO xsdRequest)
        {
            if (string.IsNullOrEmpty(xsdRequest.Xsd))
                return BadRequest("Conteúdo do XSD não fornecido.");

            string xsdContent = Encoding.UTF8.GetString(Convert.FromBase64String(xsdRequest.Xsd));

            if (string.IsNullOrEmpty(xsdContent))
                return BadRequest("Falha ao realizar o decode.");

            string result = await _xsdService.ReadXSD(xsdContent);
            return Content(result, "application/xml");
        }
    }
}
