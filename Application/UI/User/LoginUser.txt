using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class LoginUser
    {
        private readonly UserService _userService;

        public LoginUser(UserService userService)
        {
            _userService = userService;
        }

        public void Ejecutar()
        {
            Console.Clear();
            Console.WriteLine("♥♥♥♥♥♥ INICIAR SESIÓN ♥♥♥♥♥♥");

            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim() ?? string.Empty;

            var usuario = _userService.GetByEmail(email);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado. Regístrate primero.");
                return;
            }

            int intentos = 0;
            const int maxIntentos = 3;

            while (intentos < maxIntentos)
            {
                Console.Write("Contraseña: ");
                var password = Console.ReadLine()?.Trim() ?? string.Empty;

                if (usuario.password == password)
                {
                    Console.WriteLine($"Bienvenido/a {usuario.first_name} {usuario.last_name}!");
                    return; 
                }
                else
                {
                    intentos++;
                    if (intentos < maxIntentos)
                    {
                        Console.WriteLine("Contraseña incorrecta. Intenta nuevamente.");
                    }
                }
            }

            Console.WriteLine("Has excedido el número de intentos permitidos. Intenta más tarde.");
        }
    }
}
