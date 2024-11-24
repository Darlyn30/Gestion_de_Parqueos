using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresoAutoController : ControllerBase
    {
        private readonly Irepository<IngresoAuto> oRepository;

        public IngresoAutoController(Irepository<IngresoAuto> oRepository)
        {
            this.oRepository = oRepository;
        }

        [HttpGet]

        public IActionResult GetIngresos()
        {
            var result = oRepository.GetAll();
            return Ok(result);
        }

        [HttpPost]

        public void register(int id)
        {
            oRepository.SP_register(id);
            
        }

        [HttpDelete]

        public void Delete(int id, string code)
        {
            oRepository.SP_eliminarRegister(code, id);
        }

    }
}
