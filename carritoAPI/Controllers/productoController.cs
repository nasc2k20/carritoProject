using carritoAPI.Modelos;
using carritoAPI.Procedimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carritoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class productoController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<productoModelo>>> GetProductos()
        {
            var funcion = new spProductos();
            var listaProd = await funcion.MostrarProductos();

            return listaProd;
        }

        [HttpPost]
        public async Task PostProductos([FromBody] productoModelo parametros)
        {
            var funcion = new spProductos();
            await funcion.InsertarDatos(parametros);
        }

        [HttpPut("{codProducto}")]
        public async Task<ActionResult> PutProductos(int codProducto, [FromBody] productoModelo parametros)
        {
            var funcion = new spProductos();
            parametros.codProducto = codProducto;
            await funcion.EditarDatos(parametros);

            return NoContent();
        }

        [HttpDelete("{codProducto}")]
        public async Task<ActionResult> DeleteProductos(int codProducto)
        {
            var funcion = new spProductos();
            var parametros = new productoModelo();
            parametros.codProducto = codProducto;
            await funcion.EliminarDatos(parametros);

            return NoContent();
        }
    }
}
