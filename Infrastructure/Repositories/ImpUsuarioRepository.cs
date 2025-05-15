using MySql.Data.MySqlClient;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;

namespace CampusLove.Infrastructure.Repositories;

    public class ImpUsuarioRepository : IUsuarioRepository

    {
        private readonly MySqlConnection _conexion;

        public ImpUsuarioRepository(MySqlConnection conexion)
        {
            _conexion = conexion;
        }
        public Usuario? ObtenerPorEmail(string email)
        {
            string query = "SELECT * FROM usuario WHERE email = @email";

            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@email", email);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return MapUsuario(reader);
            }

            return null;
        }

        public Usuario? ObtenerPorUsuario(string username)
        {
            string query = "SELECT * FROM usuario WHERE usuario_name = @username";

            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@username", username);

            using var reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return MapUsuario(reader);
            }

            return null;
        }
        public IEnumerable<Usuario> ObtenerTodos()
        {
            var lista = new List<Usuario>();
            string query = "SELECT * FROM usuario";

            using var cmd = new MySqlCommand(query, _conexion);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(MapUsuario(reader));
            }

            return lista;
        }

        public Usuario? ObtenerPorId(int id)
        {
            string query = "SELECT * FROM usuario WHERE id = @id";
            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapUsuario(reader) : null;
        }


      
        public void Crear(Usuario u)
        {
            string query = @"INSERT INTO usuario 
                (nombre, email, clave, usuario_name, edad, genero_id, carrera_id, direccion_id, frase_perfil) 
                VALUES (@nombre, @correo, @clave, @usuario, @edad, @genero, @carrera, @direccion, @frase)";

            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@nombre", u.Nombre);
            cmd.Parameters.AddWithValue("@correo", u.Email);
            cmd.Parameters.AddWithValue("@clave", u.Clave);
            cmd.Parameters.AddWithValue("@usuario", u.UsuarioName);
            cmd.Parameters.AddWithValue("@edad", u.Edad);
            cmd.Parameters.AddWithValue("@genero", u.GeneroId);
            cmd.Parameters.AddWithValue("@carrera", u.CarreraId);
            cmd.Parameters.AddWithValue("@direccion", u.DireccionId);
            cmd.Parameters.AddWithValue("@frase", u.FrasePerfil);

            cmd.ExecuteNonQuery();
        }

        public void Actualizar(Usuario u)
        {
            string query = @"UPDATE usuario SET nombre = @nombre, email = @correo, clave = @clave,
                            usuario_name = @usuario, edad = @edad, genero_id = @genero, 
                            carrera_id = @carrera, direccion_id = @direccion, frase_perfil = @frase 
                            WHERE id = @id";

            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@nombre", u.Nombre);
            cmd.Parameters.AddWithValue("@correo", u.Email);
            cmd.Parameters.AddWithValue("@clave", u.Clave);
            cmd.Parameters.AddWithValue("@usuario", u.UsuarioName);
            cmd.Parameters.AddWithValue("@edad", u.Edad);
            cmd.Parameters.AddWithValue("@genero", u.GeneroId);
            cmd.Parameters.AddWithValue("@carrera", u.CarreraId);
            cmd.Parameters.AddWithValue("@direccion", u.DireccionId);
            cmd.Parameters.AddWithValue("@frase", u.FrasePerfil);
            cmd.Parameters.AddWithValue("@id", u.Id);

            cmd.ExecuteNonQuery();
        }

        public void Eliminar(int id)
        {
            string query = "DELETE FROM usuario WHERE id = @id";
            using var cmd = new MySqlCommand(query, _conexion);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private Usuario MapUsuario(MySqlDataReader reader)
        {
            return new Usuario
            {
                Id = Convert.ToInt32(reader["id"]),
                Nombre = reader["nombre"].ToString()!,
                Email = reader["email"].ToString()!,
                Clave = reader["clave"].ToString()!,
                UsuarioName = reader["usuario_name"].ToString()!,
                Edad = Convert.ToInt32(reader["edad"]),
                GeneroId = Convert.ToInt32(reader["genero_id"]),
                CarreraId = Convert.ToInt32(reader["carrera_id"]),
                DireccionId = Convert.ToInt32(reader["direccion_id"]),
                FrasePerfil = reader["frase_perfil"].ToString()!,
                LikesRecibidos = Convert.ToInt32(reader["likes_recibidos"]),
                LikesDisponibles = Convert.ToInt32(reader["likes_disponibles"]),
            };
        }
    }
