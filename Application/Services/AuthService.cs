using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using CampusLove.Domain.Entities;
using CampusLove.Domain.Ports;
using MySql.Data.MySqlClient;

namespace CampusLove.Application.Services
{
    public class AuthService
    {
        private readonly IUsuarioRepository _repo;        private const string ADMIN_USERNAME = "admin";
        private const string ADMIN_PASSWORD = "admin123";
        public AuthService(IUsuarioRepository repo)

        {
            _repo = repo;
        }

        public class LoginResultado
        {
            public bool Exitoso { get; set; }
            public bool EsAdmin { get; set; }
        }

        public LoginResultado Login(string usuario_email, string clave)
        {
            if (string.IsNullOrWhiteSpace(usuario_email) || string.IsNullOrWhiteSpace(clave))
            {
                Console.WriteLine("❌ Usuario y contraseña son requeridos.");
                return new LoginResultado { Exitoso = false };
            }

            if (EsAdmin(usuario_email, clave))
            {
                return new LoginResultado { Exitoso = true, EsAdmin = true };
            }

            var usuario = _repo.ObtenerPorEmail(usuario_email) ??
                          _repo.ObtenerPorUsuario(usuario_email);

            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario no encontrado.");
                return new LoginResultado { Exitoso = false };
            }

            if (usuario.Clave != clave)
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
            if (nuevoUsuario.UsuarioName.ToLower() == ADMIN_USERNAME.ToLower() ||
            nuevoUsuario.Email.ToLower() == ADMIN_USERNAME.ToLower())
            {
                Console.WriteLine("🚫 No puedes registrarte como administrador.");
                return false;
            }
            try
            {
                _repo.Crear(nuevoUsuario);
                Console.WriteLine("✅ Registro exitoso.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error al registrar: " + ex.Message);
                return false;
            }
        }
        public bool EsAdmin(string usuario, string clave)
        {
            return usuario == ADMIN_USERNAME && clave == ADMIN_PASSWORD;
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
