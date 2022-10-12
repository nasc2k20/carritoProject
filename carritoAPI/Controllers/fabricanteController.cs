using carritoAPI.Modelos;
using carritoAPI.Procedimientos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace carritoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fabricanteController : ControllerBase
    {

        [HttpGet]
        public async Task <ActionResult<List<fabricanteModelo>>> GetFabricantes()
        {
            var funcion = new spFabricantes();
            var listaFab = await funcion.MostrarFabricantes();

            return listaFab;
        }

        [HttpPost]
        public async Task PostFabricantes([FromBody] fabricanteModelo parametros)
        {
            var funcion = new spFabricantes();
            await funcion.InsertarDatos(parametros);
        }

        [HttpPut("{codFabricante}")]
        public async Task<ActionResult> PutFabricantes(int codFabricante, [FromBody] fabricanteModelo parametros)
        {
            var funcion = new spFabricantes();
            parametros.codFabricante = codFabricante;
            await funcion.EditarDatos(parametros);

            return NoContent();
        }

        [HttpDelete("{codFabricante}")]
        public async Task<ActionResult> DeleteFabricantes(int codFabricante)
        {
            var funcion = new spFabricantes();
            var parametros = new fabricanteModelo();
            parametros.codFabricante = codFabricante;
            await funcion.EliminarDatos(parametros);

            return NoContent();
        }

    }
}
