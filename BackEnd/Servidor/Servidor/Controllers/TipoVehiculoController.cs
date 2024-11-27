using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoVehiculoController : ControllerBase
    {
        private readonly Irepository<TipoAuto> oRepository;
        public TipoVehiculoController(Irepository<TipoAuto> oRepository)
        {
            this.oRepository = oRepository;
        }

        [HttpGet]

        public IActionResult GetTipoVehiculo()
        {
            var result = oRepository.GetAll();
            return Ok(result);
        }
    }
}
