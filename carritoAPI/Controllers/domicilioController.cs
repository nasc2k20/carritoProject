using carritoAPI.Modelos;
using carritoAPI.Procedimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carritoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class domicilioController : ControllerBase
    {
        [HttpGet("{codCliente}")]
        public async Task<ActionResult<List<domicilioModelo>>> GetDomicilios(int codCliente)
        {
            var funcion = new spDomicilios();
            var listaDomicilio = await funcion.MostrarDomicilios(codCliente);

            return listaDomicilio;
        }

        [HttpPost]
        public async Task PostDomicilios([FromBody] domicilioModelo parametros)
        {
            var funcion = new spDomicilios();
            await funcion.InsertarDatos(parametros);
        }

        [HttpPut("{codDomicilio}")]
        public async Task<ActionResult> PutDomicilios(int codDomicilio, [FromBody] domicilioModelo parametros)
        {
            var funcion = new spDomicilios();
            parametros.codDomicilio = codDomicilio;
            await funcion.EditarDatos(parametros);

            return NoContent();
        }

        [HttpDelete("{codDomicilio}")]
        public async Task<ActionResult> DeleteDomicilios(int codDomicilio)
        {
            var funcion = new spDomicilios();
            var parametros = new domicilioModelo();
            parametros.codDomicilio = codDomicilio;
            await funcion.EliminarDatos(parametros);

            return NoContent();
        }
    }
}
