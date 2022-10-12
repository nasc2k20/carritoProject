using carritoAPI.Datos;
using carritoAPI.Modelos;
using System.Data.SqlClient;
using System.Data;

namespace carritoAPI.Procedimientos
{
    public class spDomicilios
    {

        ConexionBd Cnn = new ConexionBd();
        public async Task<List<domicilioModelo>> MostrarDomicilios(int codCliente)
        {
            var listaDomicilio = new List<domicilioModelo>();

            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_DomicilioMostrar", querys))
                {
                    await querys.OpenAsync();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codCliente", codCliente);

                    using (var lector = await cmd.ExecuteReaderAsync())
                    {
                        while (await lector.ReadAsync())
                        {
                            var domicilioModelo = new domicilioModelo();
                            domicilioModelo.codDomicilio = (int)lector["codDomicilio"];
                            domicilioModelo.calleDom = (string)lector["calleDom"];
                            domicilioModelo.estadoDom = (string)lector["estadoDom"];
                            domicilioModelo.ciudadDom = (string)lector["ciudadDom"];
                            domicilioModelo.telefonoDom = (string)lector["telefonoDom"];
                            domicilioModelo.paisDom = (string)lector["paisDom"];
                            domicilioModelo.codCliente = (int)lector["codCliente"];
                            listaDomicilio.Add(domicilioModelo);
                        }
                    }
                }
            }

            return listaDomicilio;
        }


        public async Task InsertarDatos(domicilioModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_DomicilioInsertar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@calleDom", parametros.calleDom);
                    cmd.Parameters.AddWithValue("@estadoDom", parametros.estadoDom);
                    cmd.Parameters.AddWithValue("@ciudadDom", parametros.ciudadDom);
                    cmd.Parameters.AddWithValue("@telefonoDom", parametros.telefonoDom);
                    cmd.Parameters.AddWithValue("@paisDom", parametros.paisDom);
                    cmd.Parameters.AddWithValue("@codCliente", parametros.codCliente);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EditarDatos(domicilioModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_DomicilioActualizar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codDomicilio", parametros.codDomicilio);
                    cmd.Parameters.AddWithValue("@calleDom", parametros.calleDom);
                    cmd.Parameters.AddWithValue("@estadoDom", parametros.estadoDom);
                    cmd.Parameters.AddWithValue("@ciudadDom", parametros.ciudadDom);
                    cmd.Parameters.AddWithValue("@telefonoDom", parametros.telefonoDom);
                    cmd.Parameters.AddWithValue("@paisDom", parametros.paisDom);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

        public async Task EliminarDatos(domicilioModelo parametros)
        {
            using (var querys = new SqlConnection(Cnn.ConectarSQL()))
            {
                using (var cmd = new SqlCommand("pa_DomicilioEliminar", querys))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codDomicilio", parametros.codDomicilio);
                    await querys.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();

                }
            }
        }

    }
}
