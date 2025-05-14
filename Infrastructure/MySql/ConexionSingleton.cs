using System;
using MySql.Data.MySqlClient;

namespace CampusLove.Infrastructure.MySql
{
    public class ConexionSingleton
    {
        private static ConexionSingleton? _instancia;
        private readonly string _connectionString;
        private MySqlConnection? _conexion;

        // Constructor privado
        private ConexionSingleton()
        {
            _connectionString = "server=localhost;database=campus_love;user=root;password=root123;";
        }

        // Devuelve la instancia única
        public static ConexionSingleton Instancia
        {
            get
            {
                _instancia ??= new ConexionSingleton();
                return _instancia;
            }
        }

        // Obtiene y abre la conexión si es necesario
        public MySqlConnection ObtenerConexion()
        {
            _conexion ??= new MySqlConnection(_connectionString);

            if (_conexion.State != System.Data.ConnectionState.Open)
                _conexion.Open();

            return _conexion;
        }

        // Cierra y limpia la conexión
        public void CerrarConexion()
        {
            if (_conexion is not null && _conexion.State == System.Data.ConnectionState.Open)
            {
                _conexion.Close();
                _conexion.Dispose();
                _conexion = null;
            }
        }
    }
}
