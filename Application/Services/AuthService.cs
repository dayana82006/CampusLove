using System.Security.Cryptography;
using System.Text;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;

namespace CampusLove.Application.Services
{
    public class AuthService
    {
        private readonly IUsersRepository _repo;

        // Usuario y hash SHA256 de "admin123"
        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "admin123"; 

        public AuthService(IUsersRepository repo)
        {
            _repo = repo;
        }

        public class LoginResultado
        {
            public bool Exitoso { get; set; }
            public bool EsAdmin { get; set; }
            public Users? Usuario { get; set; }
        }

        public LoginResultado Login(string user_email, string password)
        {
            if (string.IsNullOrWhiteSpace(user_email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("❌ Usuario y contraseña son requeridos.");
                return new LoginResultado { Exitoso = false };
            }

            var emailOrUser = user_email.Trim();
            var passwordTrimmed = password.Trim();

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
            //var hashed = HashPassword(password);
            return user.Trim().ToLower() == ADMIN_USERNAME &&  password == ADMIN_PASSWORD;
        }

       /* private static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }*/
    }
}
