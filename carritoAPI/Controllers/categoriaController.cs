using carritoAPI.Modelos;
using carritoAPI.Procedimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carritoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class categoriaController : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<List<categoriaModelo>>> GetCategorias()
        {
            var funcion = new spCategorias();
            var listaCat = await funcion.MostrarCategorias();

            return listaCat;
        }

        [HttpPost]
        public async Task PostCategorias([FromBody] categoriaModelo parametros)
        {
            var funcion = new spCategorias();
            await funcion.InsertarDatos(parametros);
        }

        [HttpPut("{codCategoria}")]
        public async Task <ActionResult> PutCategorias(int codCategoria, [FromBody] categoriaModelo parametros)
        {
            var funcion = new spCategorias();
            parametros.codCategoria = codCategoria;
            await funcion.EditarDatos(parametros);

            //return Ok(funcion);
            return NoContent();
        }

        [HttpDelete("{codCategoria}")]
        public async Task<ActionResult> DeleteCategorias(int codCategoria)
        {
            var funcion = new spCategorias();
            var parametros = new categoriaModelo();
            parametros.codCategoria = codCategoria;
            await funcion.EliminarDatos(parametros);

            //return Ok(funcion);
            return NoContent();
        }
    }
}
