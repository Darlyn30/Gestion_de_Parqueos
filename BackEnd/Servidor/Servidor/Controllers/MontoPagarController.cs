using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontoPagarController : ControllerBase
    {
        private readonly Irepository<MontoPagar> oRepository;
        public MontoPagarController(Irepository<MontoPagar> oRepository)
        {
            this.oRepository = oRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            var result = oRepository.GetAll();
            return Ok(result);
        }
    }
}
