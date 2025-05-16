using System;
using System.Security.Cryptography;
using System.Text;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Interfaces;
using CampusLove.Domain.Ports;

namespace CampusLove.Application.Services
{
    public class AuthService
    {
        private readonly IUsersRepository _repo;
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

            if (EsAdmin(emailOrUser, passwordTrimmed))
            {
                return new LoginResultado { Exitoso = true, EsAdmin = true };
            }

            var user = _repo.GetByEmail(emailOrUser) ?? _repo.GetByUser(emailOrUser);
            if (user == null)
            {
                Console.WriteLine("❌ Usuario no encontrado.");
                return new LoginResultado { Exitoso = false };
            }

            var hashedInputPassword = HashPassword(passwordTrimmed);
            if (user.password != hashedInputPassword)
            {
                Console.WriteLine("❌ Contraseña incorrecta.");
                return new LoginResultado { Exitoso = false };
            }

            return new LoginResultado { Exitoso = true, EsAdmin = false };
        }

        public bool Registrar(Usuario nuevoUsuario)
        {
            if (nuevoUsuario.Edad < 16)
            {
                Console.WriteLine("🚫 Debes tener al menos 16 años para registrarte.");
                return false;
            }

            var usernameLower = nuevoUsuario.UsuarioName.Trim().ToLower();
            var emailLower = nuevoUsuario.Email.Trim().ToLower();

            if (usernameLower == ADMIN_USERNAME || emailLower == ADMIN_USERNAME)
            {
                Console.WriteLine("🚫 No puedes registrarte como administrador.");
                return false;
            }

            try
            {
                nuevoUsuario.password = HashPassword(nuevoUsuario.password.Trim());
                _repo.Create(nuevoUsuario);
                Console.WriteLine("✅ Registro exitoso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al registrar: " + ex.Message);
                return false;
            }
        }

        public bool EsAdmin(string user, string password)
        {
            return user.Trim().ToLower() == ADMIN_USERNAME && password == ADMIN_PASSWORD;
        }

        public static string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
