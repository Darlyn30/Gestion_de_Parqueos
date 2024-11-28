using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalcularTarifaController : ControllerBase
    {
        private readonly ITarifa oTarifa;
        public CalcularTarifaController(ITarifa oTarifa)
        {
            this.oTarifa = oTarifa;
        }

        [HttpGet]

        public IActionResult GetCalculo(int vehiculoId, string formato, int cantidad)
        {
            var result = oTarifa.calcTarifa(vehiculoId, formato, cantidad);
            return Ok(result);
        }
    }
}
