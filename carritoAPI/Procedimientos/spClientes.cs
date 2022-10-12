using carritoAPI.Modelos;
using System.Data.SqlClient;
using System.Data;
using carritoAPI.Datos;

namespace carritoAPI.Procedimientos
{
    public class spClientes
    {
        ConexionBd Cnn = new ConexionBd();

        public async Task<List<clienteModelo>> MostrarClientes()
        {
            var listaCliente = new List<clienteModelo>();

            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ClienteMostrar", querys))
                {
                    await querys.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            var clienteModelo = new clienteModelo();
                            clienteModelo.codCliente = (int)lector["codCliente"];
                            clienteModelo.nombreCliente = (string)lector["nombreCliente"];
                            clienteModelo.apellidoCliente = (string)lector["apellidoCliente"];
                            clienteModelo.duiCliente = (string)lector["duiCliente"];
                            clienteModelo.loginUsuario = (string)lector["loginUsuario"];
                            listaCliente.Add(clienteModelo);
                        }
                    }
                }
            }

            return listaCliente;
        }


        public async Task InsertarDatos(clienteModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ClienteInsertar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombreCliente", parametros.nombreCliente);
                    cmd.Parameters.AddWithValue("@apellidoCliente", parametros.apellidoCliente);
                    cmd.Parameters.AddWithValue("@duiCliente", parametros.duiCliente);
                    cmd.Parameters.AddWithValue("@loginUsuario", parametros.loginUsuario);
                    cmd.Parameters.AddWithValue("@loginPassword", parametros.loginPassword);
                    cmd.Parameters.AddWithValue("@nombreCompleto", parametros.nombreCliente+" "+parametros.apellidoCliente);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EditarDatos(clienteModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ClienteActualizar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCliente", parametros.codCliente);
                    cmd.Parameters.AddWithValue("@nombreCliente", parametros.nombreCliente);
                    cmd.Parameters.AddWithValue("@apellidoCliente", parametros.apellidoCliente);
                    cmd.Parameters.AddWithValue("@duiCliente", parametros.duiCliente);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EliminarDatos(clienteModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_ClienteEliminar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCliente", parametros.codCliente);
                    cmd.Parameters.AddWithValue("@loginUsuario", parametros.loginUsuario);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

    }
}
