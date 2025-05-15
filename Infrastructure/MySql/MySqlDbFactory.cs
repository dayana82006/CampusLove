using CampusLove.Domain.Factory;
using CampusLove.Domain.Ports;
using CampusLove.Infrastructure.Repositories;
using MySql.Data.MySqlClient;

namespace CampusLove.Infrastructure.MySql
{
    public class MySqlDbFactory : IDbFactory
    {
        private readonly string _connectionString;

        public MySqlDbFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IUsuarioRepository CrearUsuarioRepository()
        {
            var conexion = new MySqlConnection(_connectionString);

            return new ImpUsuarioRepository(conexion);
        }
        public IPaisRepository CrearPaisRepository()
        {
            return new ImpPaisRepository(_connectionString);
        }
       
    }
}
