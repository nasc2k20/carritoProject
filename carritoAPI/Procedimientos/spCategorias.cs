using carritoAPI.Datos;
using carritoAPI.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace carritoAPI.Procedimientos
{
    public class spCategorias
    {
        ConexionBd Cnn = new ConexionBd();
        public async Task<List<categoriaModelo>> MostrarCategorias()
        {
            var listaCateg = new List<categoriaModelo>();

            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_CategoriaMostrar", querys))
                {
                    await querys.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using(var lector = await cmd.ExecuteReaderAsync())
                    {
                        while(await lector.ReadAsync())
                        {
                            var categoriaModelo = new categoriaModelo();
                            categoriaModelo.codCategoria = (int)lector["codCategoria"];
                            categoriaModelo.nombreCategoria = (string)lector["nombreCategoria"];
                            listaCateg.Add(categoriaModelo);
                        }
                    }
                }
            }

            return listaCateg;
        }


        public async Task InsertarDatos(categoriaModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_CategoriaInsertar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreCategoria", parametros.nombreCategoria);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    
                }
            }
        }

        public async Task EditarDatos(categoriaModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_CategoriaActualizar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCategoria", parametros.codCategoria);
                    cmd.Parameters.AddWithValue("@nombreCategoria", parametros.nombreCategoria);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EliminarDatos(categoriaModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_CategoriaEliminar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCategoria", parametros.codCategoria);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }
    }
}
