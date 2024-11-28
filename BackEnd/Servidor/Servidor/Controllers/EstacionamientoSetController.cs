using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionamientoSetController : ControllerBase
    {
        private readonly IVistaDisponibilidad oVistaDisponibilidad;
        public EstacionamientoSetController(IVistaDisponibilidad oVistaDisponibilidad)
        {
            this.oVistaDisponibilidad = oVistaDisponibilidad;
        }

        [HttpGet]
        
        public IActionResult Get(string tipo)
        {
                
            var result = oVistaDisponibilidad.SetEstacionamientos(tipo);
            JsonResponse json = new JsonResponse { Mensaje = result};
            
            return Ok(json);
        }
    }
}
