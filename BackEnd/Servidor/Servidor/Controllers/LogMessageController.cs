using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogMessageController : ControllerBase
    {
        private readonly Irepository<LogsMessages> oRepository;
        public LogMessageController(Irepository<LogsMessages> oRepository)
        {
            this.oRepository = oRepository;
        }

        [HttpGet]

        public IActionResult Get()
        {
            var result = oRepository.saveLogTxt();
            return Ok(result);
        }
    }
}
