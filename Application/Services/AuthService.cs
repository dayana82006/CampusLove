using System.Security.Cryptography;
using System.Text;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class AuthService
    {
         // Campos privados para los repositorios inyectados
        private readonly IUsersRepository _repo;

        // INFO: Usuario y contraseña de "admin123 admin seria el unico con acceso al panel de administracion
        //de la aplicacion, podra acceder a los usuarios, paises, carreras, etc
        //y podra eliminar, agregar o modificar los datos de los mismos.
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "admin123"; 

        
        public AuthService(IUsersRepository repo)
        {
            _repo = repo;
        }

        // Método para definir el resultado del login
        public class LoginResultado
        {
            public bool Exitoso { get; set; }
            public bool EsAdmin { get; set; }
            public Users? Usuario { get; set; }
        }

        // Método para iniciar sesión
        // Recibe el email y la contraseña
        public LoginResultado Login(string user_email, string password)
        {
            if (string.IsNullOrWhiteSpace(user_email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ Usuario y contraseña son requeridos.");
                return new LoginResultado { Exitoso = false };
            }

            var emailOrUser = user_email.Trim();
            var passwordTrimmed = password.Trim();

            // Verifica si es un administrador
            if (EsAdmin(emailOrUser, password))
            {
                return new LoginResultado { Exitoso = true, EsAdmin = true };
            }

            var user = _repo.GetByEmail(emailOrUser) ?? _repo.GetByUser(emailOrUser);

            if (user == null)
            {
                return new LoginResultado { Exitoso = false };
            }


            if (user.password != password)
            {
                return new LoginResultado { Exitoso = false };
            }

            return new LoginResultado { Exitoso = true, EsAdmin = false, Usuario = user };
        }

        public bool EsAdmin(string user, string password)
        {
            return user.Trim().ToLower() == ADMIN_USERNAME &&  password == ADMIN_PASSWORD;
        }

    }
}
