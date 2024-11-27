using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontoController : ControllerBase
    {
        private readonly ITarifa oTarifa;
        public MontoController(ITarifa oTarifa)
        {
            this.oTarifa = oTarifa;
        }

        [HttpGet]

        public IActionResult GetMonto(DateTime horaEntrada, int vehiculoId)
        {
            var result = oTarifa.GetMonto(horaEntrada, vehiculoId);
            return Ok(result);
        }
    }
}
