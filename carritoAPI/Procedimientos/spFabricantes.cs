using carritoAPI.Datos;
using carritoAPI.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace carritoAPI.Procedimientos
{
    public class spFabricantes
    {
        ConexionBd Cnn = new ConexionBd();

        public async Task<List<fabricanteModelo>> MostrarFabricantes()
        {
            var listaFabricante = new List<fabricanteModelo>();

            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_FabricanteMostrar", querys))
                {
                    await querys.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            var fabricanteModelo = new fabricanteModelo();
                            fabricanteModelo.codFabricante = (int)lector["codFabricante"];
                            fabricanteModelo.nombreFabricante = (string)lector["nombreFabricante"];
                            listaFabricante.Add(fabricanteModelo);
                        }
                    }
                }
            }

            return listaFabricante;
        }

        public async Task InsertarDatos(fabricanteModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_FabricanteInsertar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreFabricante", parametros.nombreFabricante);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EditarDatos(fabricanteModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_FabricanteActualizar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codFabricante", parametros.codFabricante);
                    cmd.Parameters.AddWithValue("@nombreFabricante", parametros.nombreFabricante);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EliminarDatos(fabricanteModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_FabricanteEliminar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codFabricante", parametros.codFabricante);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

    }
}
