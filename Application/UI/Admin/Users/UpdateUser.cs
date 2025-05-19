using System;
using CampusLove.Application.Services;

namespace CampusLove.Application.UI.User
{
    public class UpdateUser
    {
        private readonly UserService _servicio;

        public UpdateUser(UserService servicio)
        {
            _servicio = servicio;
        }

        public void Ejecutar()
        {
            Console.Clear();
            Console.WriteLine("✏️ Actualizar usuario");

            Console.Write("Ingrese el ID del usuario a actualizar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("❌ ID no válido.");
                return;
            }

            var usuario = _servicio.ObtenerPorId(id);
            if (usuario == null)
            {
                Console.WriteLine("❌ Usuario no encontrado.");
                return;
            }

            Console.WriteLine("Deje en blanco cualquier campo que no desee modificar.\n");

            Console.Write($"Nombre actual ({usuario.first_name}): ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) usuario.first_name = input;

            Console.Write($"Apellido actual ({usuario.last_name}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) usuario.last_name = input;

            Console.Write($"Email actual ({usuario.email}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) usuario.email = input;

            Console.Write($"Contraseña actual ({usuario.password}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) usuario.password = input;

            Console.Write($"Fecha de nacimiento actual ({usuario.birth_date.ToShortDateString()}): ");
            input = Console.ReadLine();
            if (DateTime.TryParse(input, out DateTime nuevaFecha)) usuario.birth_date = nuevaFecha;

            Console.Write($"ID Género actual ({usuario.id_gender}): ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int nuevoGenero)) usuario.id_gender = nuevoGenero;

            Console.Write($"ID Carrera actual ({usuario.id_career}): ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int nuevaCarrera)) usuario.id_career = nuevaCarrera;

            Console.Write($"ID Dirección actual ({usuario.id_address}): ");
            input = Console.ReadLine();
            if (int.TryParse(input, out int nuevaDireccion)) usuario.id_address = nuevaDireccion;

            Console.Write($"Frase de perfil actual ({usuario.profile_phrase}): ");
            input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input)) usuario.profile_phrase = input;

            _servicio.Actualizar(usuario);
            Console.WriteLine("\n✅ Usuario actualizado correctamente.");
            Console.ReadKey();
        }
    }
}
