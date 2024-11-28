using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VistaDisponibilidadController : ControllerBase
    {
        private readonly IVistaDisponibilidad oVistaDisponibilidad;
        public VistaDisponibilidadController(IVistaDisponibilidad oVistaDisponibilidad)
        {
            this.oVistaDisponibilidad = oVistaDisponibilidad;
        }

        [HttpGet]

        public IActionResult GetDisponibilad()
        {
            var result = oVistaDisponibilidad.GetInfo();
            return Ok(result);
        }
    }
}
