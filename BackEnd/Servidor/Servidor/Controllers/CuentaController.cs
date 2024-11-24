using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Servidor.Interfaces;
using Servidor.Models;

namespace Servidor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly IAdmin oAdmin;
        public CuentaController(IAdmin oAdmin)
        {
            this.oAdmin = oAdmin;
        }

        [HttpGet]

        public IActionResult GetCuentas()
        {
            var result = oAdmin.GetCuentas();
            return Ok(result);
        }

        [HttpPost]

        public void Set(Cuenta model)
        {
            oAdmin.Set(model);
        }
    }
}
