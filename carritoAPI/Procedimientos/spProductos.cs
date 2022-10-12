using carritoAPI.Datos;
using carritoAPI.Modelos;
using System.Data;
using System.Data.SqlClient;

namespace carritoAPI.Procedimientos
{
    public class spProductos
    {

        ConexionBd Cnn = new ConexionBd();

        public async Task<List<productoModelo>> MostrarProductos()
        {
            var listaProducto = new List<productoModelo>();

            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ProductoMostrar", querys))
                {
                    await querys.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            var productoModelo = new productoModelo();
                            productoModelo.codProducto = (int)lector["codProducto"];
                            productoModelo.nombreProducto = (string)lector["nombreProducto"];
                            productoModelo.descripcionProd = (string)lector["descripcionProd"];
                            productoModelo.precioProd = (decimal)lector["precioProd"];
                            productoModelo.marcaProd = (string)lector["marcaProd"];
                            productoModelo.unidadesProd = (int)lector["unidadesProd"];
                            productoModelo.fotoProducto = (string)lector["fotoProducto"];
                            productoModelo.codCategoria = (int)lector["codCategoria"];
                            productoModelo.codFabricante = (int)lector["codFabricante"];
                            listaProducto.Add(productoModelo);
                        }
                    }
                }
            }

            return listaProducto;
        }

        public async Task InsertarDatos(productoModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ProductoInsertar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreProducto", parametros.nombreProducto);
                    cmd.Parameters.AddWithValue("@descripcionProd", parametros.descripcionProd);
                    cmd.Parameters.AddWithValue("@precioProd", parametros.precioProd);
                    cmd.Parameters.AddWithValue("@marcaProd", parametros.marcaProd);
                    cmd.Parameters.AddWithValue("@unidadesProd", parametros.unidadesProd);
                    cmd.Parameters.AddWithValue("@fotoProducto", parametros.fotoProducto);
                    cmd.Parameters.AddWithValue("@codCategoria", parametros.codCategoria);
                    cmd.Parameters.AddWithValue("@codFabricante", parametros.codFabricante);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EditarDatos(productoModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ProductoActualizar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codProducto", parametros.codProducto);
                    cmd.Parameters.AddWithValue("@nombreProducto", parametros.nombreProducto);
                    cmd.Parameters.AddWithValue("@descripcionProd", parametros.descripcionProd);
                    cmd.Parameters.AddWithValue("@precioProd", parametros.precioProd);
                    cmd.Parameters.AddWithValue("@marcaProd", parametros.marcaProd);
                    cmd.Parameters.AddWithValue("@unidadesProd", parametros.unidadesProd);
                    cmd.Parameters.AddWithValue("@fotoProducto", parametros.fotoProducto);
                    cmd.Parameters.AddWithValue("@codCategoria", parametros.codCategoria);
                    cmd.Parameters.AddWithValue("@codFabricante", parametros.codFabricante);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EliminarDatos(productoModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ProductoEliminar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codProducto", parametros.codProducto);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }


    }
}
