using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SesionController : ControllerBase
    {
        private readonly ISesion oSesion;
        public SesionController(ISesion oSesion)
        {
            this.oSesion = oSesion;
        }

        [HttpGet]

        public IActionResult Get()
        {
            var result = oSesion.sesionIniciada();
            return Ok(result);
        }

        [HttpPost]

        public void keepSesion(string email)
        {
            oSesion.keepSesion(email);
        }

        [HttpDelete]

        public void deleteSesion(string email)
        {
            oSesion.deleteSesion(email);
        }
    }
}
