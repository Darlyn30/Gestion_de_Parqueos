using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionamientoController : ControllerBase
    {
        private readonly Irepository<Estacionamientos> oEstacionamiento;

        public EstacionamientoController(Irepository<Estacionamientos> oEstacionamiento)
        {
            this.oEstacionamiento = oEstacionamiento;
        }

        [HttpGet]

        public IActionResult GetInfo()
        {
            var result = oEstacionamiento.GetAll();
            return Ok(result);
        }


    }
}
