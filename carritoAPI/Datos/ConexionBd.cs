namespace carritoAPI.Datos
{
    public class ConexionBd
    {
        private string Conexion = String.Empty;

        public ConexionBd()
        {
            var Constructor = new ConfigurationBuilder().SetBasePath(
                Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            Conexion = Constructor.GetSection("ConnectionStrings:conexion").Value;
        }

        public string ConectarSQL()
        {
            return Conexion;
        }
    }
}
