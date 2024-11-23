using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroVistaAdminController : ControllerBase
    {
        private readonly IAdmin oAdmin;
        public RegistroVistaAdminController(IAdmin oAdmin)
        {
            this.oAdmin = oAdmin;
        }

        [HttpGet]

        public IActionResult getInfo()
        {
            var result = oAdmin.GetCuentas();
            return Ok(result);
        }
    }
}
