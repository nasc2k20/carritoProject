using carritoAPI.Modelos;
using carritoAPI.Procedimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carritoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class clienteController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<clienteModelo>>> GetClientes()
        {
            var funcion = new spClientes();
            var listaClientes = await funcion.MostrarClientes();

            return listaClientes;
        }

        [HttpPost]
        public async Task PostClientes([FromBody] clienteModelo parametros)
        {
            var funcion = new spClientes();
            await funcion.InsertarDatos(parametros);
        }

        [HttpPut("{codCliente}")]
        public async Task<ActionResult> PutClientes(int codCliente, [FromBody] clienteModelo parametros)
        {
            var funcion = new spClientes();
            parametros.codCliente = codCliente;
            await funcion.EditarDatos(parametros);

            return NoContent();
        }

        [HttpDelete("{codCliente}")]
        public async Task<ActionResult> DeleteClientes(int codCliente)
        {
            var funcion = new spClientes();
            var parametros = new clienteModelo();
            parametros.codCliente = codCliente;
            await funcion.EliminarDatos(parametros);

            return NoContent();
        }

    }
}
